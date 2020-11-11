Imports System.Math
Imports System
Imports System.Numerics
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form1
    Dim omega, x0, elem, ElecField, ti, mass, LightSpeed, WaveLength, Cycle, tc, v, energy, action, ev, h, difference As Double
    Dim Time_initial As New List(Of Double)
    Dim Time_collision As New List(Of Double)
    Dim KE As New List(Of Double)
    Dim Sayou As New List(Of Double)
    Dim Positions As New List(Of List(Of Double))
    Dim Differences As New List(Of Double)
    Dim D_Dealed As New List(Of Double)
    Dim Tc_Dealed As New List(Of Double)
    Dim D_trans As New List(Of Double)
    Dim Waves As New List(Of Double)
    Dim A As New List(Of Double)
    Dim B As New List(Of Double)
    Dim En As New List(Of Double)

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar < "0"c OrElse "9"c < e.KeyChar Then
            e.Handled = True
            Me.Text = ""
        End If
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        x0 = 0
        elem = 1.60217662 * 10 ^ (-19)
        ElecField = 274.6 * Sqrt(2) * 10 ^ 9
        mass = 9.10938356 * 10 ^ (-31)
        LightSpeed = 3.0 * 10 ^ 8
        WaveLength = 800 * 10 ^ (-9)
        Cycle = WaveLength / LightSpeed
        omega = 2 * Math.PI * LightSpeed / WaveLength
        ev = 6.242 * 10 ^ 18
        ti = 0
        h = 6.62607015 * 10 ^ (-34) * ev
        For ti = 0 To 0.665 * 10 ^ (-15) Step 0.0001 * 10 ^ (-15)
            FindTc(ti, tc)
            CalcVel(tc, ti, v)
            CalcEnergy(v, energy)
            CalcAction(ti, tc, action)
            CalcDifference(action, difference)
            Console.WriteLine(ti & "," & tc & "," & energy)
            If tc <> 0 Then
                Time_initial.Add(ti)
                Time_collision.Add(tc)
                KE.Add(energy)
                Sayou.Add(action)
                Differences.Add(difference)
            End If
        Next
        For i = 0 To Time_initial.Count - 2 Step 1
            DataGridView1.Rows.Add({Time_initial(i).ToString, Time_collision(i).ToString, KE(i).ToString, Sayou(i).ToString})
        Next
        Shortize()
        FT()
        setChart(Chart1, Time_initial, Time_collision, "Ti[fs]", "Tc[fs]", "図1 再衝突時間Tcとトンネルイオン化時間Tiの関係")
        setChart(Chart2, Time_initial, KE, "Ti", "KE[eV]", "図2 運動エネルギーKEとトンネルイオン化時間Tiの関係")
        setChart(Chart3, Time_collision, Sayou, "Tc", "S[eV・s]", "図3 作用Sと再衝突時間Tcの関係")
        setChart(Chart4, Tc_Dealed, D_Dealed, "Tc", "d(t)", "図4 d(t)")
        setChart(Chart5, Waves, D_trans, "w", "D(w)", "図5 D(w)")
        With Chart4.ChartAreas(0)
            With .AxisY
                .Maximum = 2
                .Minimum = -2
            End With
        End With
        Chart5.ChartAreas(0).AxisX.Interval = Waves(D_trans.IndexOf(D_trans.Max))
        Chart5.ChartAreas(0).AxisX.IsLabelAutoFit = True
        Chart4.Series(0).MarkerSize = 2



    End Sub

    Function E(ByVal t As Double)
        Return Math.Cos(t * omega)
    End Function

    Function x(ByVal argt As Double, ByVal argti As Double)
        Dim xt As Double
        xt = x0 - elem * ElecField * (E(argt) - E(argti) + omega * (argt - argti) * Math.Sin(omega * argti)) / (mass * omega * omega)
        Return xt
    End Function

    Sub FindTc(ByRef argti As Double, ByRef argtc As Double)
        Dim temp_t As Double
        Dim prex As Double
        For temp_t = argti + Cycle / 100 To argti + 1.5 * Cycle Step Cycle / 10000
            Dim nowx As Double = x(temp_t, argti)
            If temp_t > argti + 1.2 * Cycle Then
                argtc = 0
                Exit For
            End If
            If nowx <= 0 And prex > 0 Then
                argtc = temp_t
                Exit For
            End If
            prex = nowx
        Next
    End Sub

    Sub CalcVel(ByVal argt As Double, ByVal argti As Double, ByRef argvelocity As Double)
        argvelocity = elem * ElecField * (Math.Sin(omega * argt) - Math.Sin(omega * argti)) / (omega * mass)
    End Sub

    Sub CalcEnergy(ByVal argvelocity As Double, ByRef argEnergy As Double)
        argEnergy = 0.5 * mass * (argvelocity ^ 2) * ev
    End Sub

    Sub CalcAction(ByVal argti As Double, ByVal argtc As Double, ByRef action As Double)
        action = (elem ^ 2 * ElecField ^ 2 / (2 * mass * omega ^ 2)) * ((argtc - argti) / 2 - (Sin(2 * omega * argtc) - Sin(2 * omega * argti)) / (4 * omega) + 2 * Sin(omega * argti) * (Cos(omega * argtc) - Cos(omega * argti)) / omega + (argtc - argti) * (Sin(omega * argti)) ^ 2) * ev
        If argtc = 0 Then
            action = 0
        End If
    End Sub

    Sub CalcDifference(ByVal argaction As Double, ByRef diff As Double)
        diff = Cos(2 * PI * argaction / h)
    End Sub

    Sub Shortize()
        Dim index_max = KE.IndexOf(KE.Max)
        Dim tc_max = Time_initial(index_max)
        Dim shortized_Differences As New List(Of Double)
        Dim Count_of_half_unit = Time_initial.Count - index_max
        Dim tc_offset As New Double
        Dim Cycle_offset As New Double
        tc_offset = 0
        Cycle_offset = 0
        For g = 0 To 3 Step 1
            For i = Differences.Count - 1 To index_max Step -1
                D_Dealed.Add(Differences(i))
                Tc_Dealed.Add(Time_collision(i) + tc_offset + Cycle_offset)
            Next
            tc_offset += tc_max

            Cycle_offset += Cycle / 2
            For j = Differences.Count - 1 To index_max Step -1
                D_Dealed.Add(Differences(j) * (-1))
                Tc_Dealed.Add(Time_collision(j) + tc_offset + Cycle_offset)
            Next
            tc_offset += tc_max


            Cycle_offset += Cycle / 2
        Next

    End Sub

    Sub FT()
        Dim index_max = KE.IndexOf(KE.Max)
        Dim tc_max = Time_collision(index_max)
        Dim cnt = D_Dealed.Count / 4
        Dim N = Tc_Dealed(cnt - 1) - Tc_Dealed(0)
        Dim delt_tc As New Double
        Dim temp_tc As New Double
        For g = 0 To 16 Step 0.001
            Dim F As New Double
            Dim an As New Double
            Dim bn As New Double
            an = 0
            bn = 0
            delt_tc = 0
            temp_tc = 0
            For i = 0 To 4 * cnt - 1 Step 1
                an += 2 / N * D_Dealed(i) * Cos(2 * PI * g * Tc_Dealed(i) / N) * delt_tc
                bn += 2 / N * D_Dealed(i) * Sin(2 * PI * g * Tc_Dealed(i) / N) * delt_tc
                delt_tc = Abs(Tc_Dealed(i) - temp_tc)
                If i Mod cnt / 2 - 1 = 0 Then
                    delt_tc = 0
                End If
            Next

            F = Sqrt(an ^ 2 + bn ^ 2)
            D_trans.Add(F)
            Waves.Add(g)
        Next

    End Sub

    Sub setChart(ByRef chart As Chart, ByVal x As List(Of Double), ByVal y As List(Of Double), ByVal x_label As String, ByVal y_label As String, ByVal title As String)
        chart.Series.Clear()
        chart.Series.Add(CType(y_label, String))
        chart.Series(CType(y_label, String)).ChartType = DataVisualization.Charting.SeriesChartType.Point
        chart.Titles.Add(title)
        chart.Series(CType(y_label, String)).MarkerSize = 2

        With chart.Titles.Item(0)
            .Position.Auto = True
            .Alignment = Drawing.ContentAlignment.BottomCenter
            .Docking = Docking.Bottom
        End With

        For i = 0 To y.Count - 1 Step 1
            chart.Series(CType(y_label, String)).Points.AddXY(x(i), y(i))
        Next

        With chart.ChartAreas(0)
            With .AxisX
                .Minimum = 0
                .Maximum = x.Max * 1.1
                .Interval = 1 * 10 ^ (-15)
                .Title = x_label

            End With

            With .AxisY
                .Maximum = y.Max * 1.1
                .Title = y_label
            End With
        End With
    End Sub

End Class
