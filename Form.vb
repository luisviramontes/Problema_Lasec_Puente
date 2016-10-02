Imports System.Threading
Public Class Form1
    Inherits System.Windows.Forms.Form
    Public VehiculoIzq, VehiculoDer, N, Y, N2, Y2 As Integer
    Public Puente As Boolean = True
    Dim Contador As Integer = 1
    Dim Contador2 As Integer = 1
    Dim Hilo1 As Threading.Thread

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Close()
    End Sub

    Dim Hilo2 As Threading.Thread

    Public Sub Hilos()
        Control.CheckForIllegalCrossThreadCalls = False
        ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = False
        Hilo1 = New Threading.Thread(AddressOf Proceso1)
        Hilo2 = New Threading.Thread(AddressOf Proceso2)
        If Hilo1.ThreadState <> Threading.ThreadState.Running Then
            Hilo1.Start()
        End If
        If VehiculoDer > 0 Then
            Hilo2.Start()
        End If
        If VehiculoIzq <= 0 And VehiculoDer <= 0 And Puente = True Then
            Hilo1.Abort()
            Hilo2.Abort()
            Hilo2.Interrupt()
            MessageBox.Show("Todos los autos han cruzado el puente ")
            btnenviar.Enabled = False
        End If


    End Sub

    Public Sub btnenviar_Click(sender As Object, e As EventArgs) Handles btnenviar.Click
        VehiculoIzq = Val(txtIzquierda.Text)
        VehiculoDer = Val(txtDerecha.Text)
        Do While Contador <= VehiculoIzq
            ListBox1.Items.Add("Vehiculo En Sentido a la Izquierda " & Contador)
            Contador = Contador + 1
        Loop

        Do While Contador2 <= VehiculoDer
            ListBox2.Items.Add("Vehiculo En Sentido a la Derecha " & Contador2)
            Contador2 = Contador2 + 1
        Loop
        Call Hilos()
    End Sub

    Public Sub Proceso1()
        Do While VehiculoIzq > 0 And Puente = True
            Puente = False
            txtestado.Text = "Ocupado"
            N = (VehiculoIzq - 1) * Rnd() + 1
            N2 = N - 1
            ListBox1.SelectedIndex = N2
            ListBox1.SelectedItem = N2
            ListBox3.Items.Add("El " & ListBox1.SelectedItem & " -> Entra por Izquierda " & vbCrLf)
            ListBox3.Items.Add("El " & ListBox1.SelectedItem & " -> Pasa Por el Puente " & vbCrLf)
            ListBox3.Items.Add("El " & ListBox1.SelectedItem & " -> Paso al lado Derecho")
            ListBox3.Items.Add("____________________________________________________")
            ListBox2.Items.Add(ListBox1.SelectedItem)
            ListBox1.Items.Remove(ListBox1.SelectedItem)
            VehiculoIzq = VehiculoIzq - 1
            Puente = True
            Thread.Sleep(4000)
            txtestado.Text = "Libre"
        Loop
    End Sub
    Public Sub Proceso2()
        Do While VehiculoDer > 0 And Puente = True
            txtestado.Text = "Ocupado"
            Puente = False
            Y = (VehiculoDer - 1) * Rnd() + 1
            Y2 = Y - 1
            ListBox2.SelectedIndex = Y2
            ListBox2.SelectedItem = Y2
            ListBox3.Items.Add("El " & ListBox2.SelectedItem & "-> Entra por Derecha" & vbCrLf)
            ListBox3.Items.Add("El " & ListBox2.SelectedItem & "-> Pasa Por el Puente " & vbCrLf)
            ListBox3.Items.Add("El " & ListBox2.SelectedItem & "-> Paso al lado izquierdo")
            ListBox3.Items.Add("____________________________________________________")
            ListBox1.Items.Add(ListBox2.SelectedItem)
            ListBox2.Items.Remove(ListBox2.SelectedItem)
            VehiculoDer = VehiculoDer - 1
            Puente = True
            Thread.Sleep(2351)
            txtestado.Text = "Libre"
        Loop
        Call Hilos()
    End Sub
End Class
