Imports System.Drawing
Imports System.Math
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.IO
Public Class Form1

    Public Xran, Yran, FileMax As New Integer
    Dim Dataname As String

    'この値x2の幅で真ん中のデータを積算
    Dim Total_width As Integer
    Dim Strength_width As Integer
    Dim MaxR = 0

    'データの入れ物たち
    Dim Datas() As MyData2D
    Dim Datas_pols() As MyData2D
    Dim TotalXs() As MyData1D
    Dim TotalYs() As MyData1D
    Dim TotalRs() As MyData1D
    Dim TotalThetas() As MyData1D

    Dim BG As Boolean
    Dim Background(,) As Double

    Dim unitedTotalX As MyData2D
    Dim unitedTotalY As MyData2D
    Dim unitedTotalR As MyData2D
    Dim unitedTotalTheta As MyData2D

    Dim sections As Int64
    Dim Signal_Strength() As MyData2D
    Dim signal11, signal12, signal13, signal14, signal15, signal16 As MyData2D

    Dim Signal_Corrected() As List(Of (Double, Double))

    Const s11 = 315
    Const f11 = 340
    Const s12 = 350
    Const f12 = 370
    Const s13 = 380
    Const f13 = 395
    Const s14 = 405
    Const f14 = 415
    Const s15 = 420
    Const f15 = 430

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


        OpenFileDialog3.Title = "Background"
        OpenFileDialog3.FileName = "D1.dat"
        OpenFileDialog3.RestoreDirectory = True
        OpenFileDialog3.ShowDialog()
        TextBox8.Text = OpenFileDialog3.FileName
        BG = True
    End Sub

    Private Sub Mouse_Location(sender As Object, e As KeyEventArgs) Handles TextBox10.KeyDown
        Dim PosX = System.Windows.Forms.Cursor.Position.X
        Dim PosY = System.Windows.Forms.Cursor.Position.Y
        If e.KeyCode = Keys.Space Then
            TextBox9.Text = (PictureBox1.Location.X - PosX).ToString()
            TextBox10.Text = (PictureBox1.Location.Y - PosY).ToString()
            TextBox11.Text = (PictureBox2.Location.X - PosX).ToString()
            TextBox12.Text = (PictureBox2.Location.X - PosY).ToString()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim PathOP As String
        Dim fns As String
        OpenFileDialog1.Title = "HHGspectrum"
        OpenFileDialog1.FileName = "test1.dat"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.ShowDialog()
        PathOP = OpenFileDialog1.FileName
        fns = PathOP
        PathOP = PathOP.Remove(PathOP.Length - 5)
        Dataname = PathOP.Last
        TextBox1.Text = PathOP

        Dim tn As String
        Dim ftn As String
        Dim fn As String

        Xran = Integer.Parse(TextBox2.Text)
        Yran = Integer.Parse(TextBox3.Text)
        FileMax = Integer.Parse(TextBox4.Text)

        '配列のサイズ指定
        ReDim Datas(FileMax)
        ReDim Datas_pols(FileMax)
        ReDim TotalXs(FileMax)
        ReDim TotalYs(FileMax)
        ReDim TotalRs(FileMax)
        ReDim TotalThetas(FileMax)
        ReDim Background(Xran, Yran)

        Dim sute, suteA As String
        Dim suteD As Double
        tn = TextBox1.Text

        PictureBox1.Width = Xran
        PictureBox1.Height = Yran


        If BG Then
            Try
                FileSystem.FileOpen(1, TextBox8.Text, OpenMode.Input)
            Catch ex As System.IO.FileNotFoundException
                Console.WriteLine("Cant read background")
                Exit Sub
            End Try

            Dim bg, bgA As String
            Dim bgD As Double
            For x = 0 To Xran - 1 Step 1
                For y = 0 To Yran - 1 Step 1
                    FileSystem.Input(1, bg)
                    bgD = Integer.Parse(bg)
                    Console.WriteLine(y)
                    Background(x, y) = Integer.Parse(bg)
                Next
                FileSystem.Input(1, bgA)
            Next

            FileSystem.FileClose(1)
        Else
            For x = 0 To Xran - 1 Step 1
                For y = 0 To Yran - 1 Step 1
                    Background(x, y) = 0
                Next
            Next
        End If
        '##### file name ####
        For ff = 1 To FileMax '### for a file
            Dim pdata(Xran, Yran) As Double
            ftn = ff.ToString
            fn = tn + ftn + ".dat" '#### file name "

            '#### open fine ####


            Try
                FileSystem.FileOpen(1, fn, OpenMode.Input)
            Catch ex As System.IO.FileNotFoundException
                TextBox1.Text = "Enter the file path"
                Exit Sub
            End Try

            For i = 0 To Xran - 1 Step 1 '## X number '##

                For j = 0 To Yran - 1 Step 1 '### Y number  ##
                    Input(1, sute)
                    suteD = Integer.Parse(sute)

                    pdata(i, j) = suteD - Background(i, j) '### pdata ##

                Next
                Input(1, suteA)
            Next
            Datas(ff - 1) = New MyData2D(Xran, Yran, PictureBox1, "VMIの可視化画像(X-Y)", "X", "Y")
            Datas(ff - 1).setData(pdata)
            FileSystem.FileClose(1)
        Next


        For i = 0 To FileMax - 1 Step 1
            Datas(i).draw()
        Next

        PictureBox1.Image = Datas(0).Image
        PictureBox1.Refresh()
        polarize()
        extract()
        unite()

        Button2.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim PathOP As String
        Dim fns As String
        OpenFileDialog2.Filter = "Folider|."
        OpenFileDialog2.Title = "Save bmp"
        OpenFileDialog2.FileName = ""
        OpenFileDialog2.CheckFileExists = False
        OpenFileDialog2.ShowDialog()
        PathOP = OpenFileDialog2.FileName
        fns = System.IO.Path.GetDirectoryName(PathOP)

        For i = 0 To FileMax - 1 Step 1
            Datas(i).Image.Save(PathOP + Dataname + (i + 1).ToString + ".bmp")
        Next

        sections = Integer.Parse(TextBox7.Text)
        For i = 0 To sections - 1 Step 1
            Using writer = New StreamWriter(PathOP + Signal_Strength(i).dataName + ".xls", False)
                For x = 0 To Signal_Corrected(i).Count - 1 Step 1
                    writer.WriteLine(x.ToString + "," + Signal_Corrected(i)(x).Item2.ToString + "," + Signal_Corrected(i)(x).Item1.ToString)
                Next
            End Using
        Next
    End Sub




    Private Sub extract()
        Total_width = Integer.Parse(TextBox5.Text)
        For d = 0 To FileMax - 1 Step 1
            'x方向の積算
            Dim xTotal As New MyData1D(Yran, PictureBox3, 1)
            For y = 0 To Yran - 1 Step 1
                Dim sum = 0
                For x = Datas(d).CenterX + (-1) * Total_width To Datas(d).CenterX + Total_width Step 1
                    sum += Datas(d).Data(x, y)

                Next
                xTotal.Data(y) = sum / (Total_width * 2)
            Next

            'y方向の積算
            Dim yTotal As New MyData1D(Xran, PictureBox4, 0)
            For x = 0 To Xran - 1 Step 1
                Dim sum = 0
                For y = Datas(d).CenterY + (-1) * Total_width To Datas(d).CenterY + Total_width Step 1
                    sum += Datas(d).Data(x, y)
                Next
                yTotal.Data(x) = sum / (Total_width * 2)
            Next

            'r方向の積算
            Dim rTotal As New MyData1D(Datas_pols(d).Yran, PictureBox5, 1)
            For theta = 0 To Datas_pols(d).Yran - 1 Step 1
                Dim sum = 0
                For r = Int(Datas_pols(d).Xran / 2) + (-1) * Total_width To Int(Datas_pols(d).Xran / 2) + Total_width Step 1
                    sum += Datas_pols(d).Data(r, theta)
                Next
                rTotal.Data(theta) = sum / (Total_width * 2)
            Next

            'θ方向の積算
            Dim thetaTotal As New MyData1D(Datas_pols(d).Xran, PictureBox6, 0)
            For r = 0 To Datas_pols(d).Xran - 1 Step 1
                Dim sum = 0
                For theta = Int(Datas_pols(d).Yran / 2) + (-1) * Total_width To Int(Datas_pols(d).Yran / 2) + Total_width Step 1
                    sum += Datas_pols(d).Data(r, theta)
                Next
                thetaTotal.Data(r) = sum / (Total_width * 2)
            Next
            TotalXs(d) = xTotal
            TotalYs(d) = yTotal
            TotalRs(d) = rTotal
            TotalThetas(d) = thetaTotal
            TotalXs(d).draw()
            TotalYs(d).draw()
            TotalRs(d).draw()
            TotalThetas(d).draw()
            ComboBox1.Items.Add(Dataname + (d + 1).ToString)
        Next

    End Sub


    Private Sub polarize()
        For d = 0 To FileMax - 1 Step 1
            Dim xl = Min(Datas(d).CenterX, Datas(d).Xran - Datas(d).CenterX)
            Dim yl = Min(Datas(d).CenterY, Datas(d).Yran - Datas(d).CenterY)
            Dim rmax = Min(xl, yl)
            If rmax > MaxR Then
                MaxR = rmax
            End If

            Dim poldata(rmax + 1, 360) As Double
            Datas_pols(d) = New MyData2D(rmax + 1, 360, PictureBox2, "VMIの可視化画像(r-θ)", "r", "θ")
            For r = 0 To rmax Step 1
                For theta = 0 To 359 Step 1
                    Dim x = Int(r * Cos(theta * PI / 180)) + Datas(d).CenterX
                    Dim y = Int(r * Sin(theta * PI / 180)) + Datas(d).CenterY
                    poldata(r, theta) = Datas(d).Data(x, y)
                Next
            Next
            Datas_pols(d).setData(poldata)
            Datas_pols(d).draw()
        Next
    End Sub


    Private Sub unite()
        unitedTotalX = New MyData2D(FileMax, Yran, PictureBox7, "中心からx方向±" + Total_width.ToString + "以内のデータの積算", "FileNo", "Y")
        Dim unitex(FileMax, Yran) As Double
        For d = 0 To FileMax - 1 Step 1
            For y = 0 To Yran - 1 Step 1
                unitex(d, y) = TotalXs(d).Data(y)
            Next
        Next
        unitedTotalX.setData(unitex)

        unitedTotalY = New MyData2D(Xran, FileMax, PictureBox8, "中心からy方向±" + Total_width.ToString + "以内のデータの積算", "X", "FileNo")
        Dim unitey(Xran, FileMax) As Double
        For d = 0 To FileMax - 1 Step 1
            For x = 0 To Xran - 1 Step 1
                unitey(x, d) = TotalYs(d).Data(x)
            Next
        Next
        unitedTotalY.setData(unitey)

        unitedTotalR = New MyData2D(FileMax, 360, PictureBox9, "中心から一定の距離rにあるデータの積算", "FileNo", "θ")
        Dim uniter(FileMax, 360) As Double
        For d = 0 To FileMax - 1 Step 1
            For theta = 0 To 359 Step 1
                uniter(d, theta) = TotalRs(d).Data(theta)
            Next
        Next
        unitedTotalR.setData(uniter)

        unitedTotalTheta = New MyData2D(MaxR, FileMax, PictureBox10, "中心から一定の角度θにあるデータの積算", "r", "FileNo")
        Dim unitetheta(MaxR, FileMax) As Double
        For d = 0 To FileMax - 1 Step 1
            For r = 0 To MaxR Step 1
                If r >= TotalThetas(d).Data.Length Then
                    unitetheta(r, d) = 0
                Else
                    unitetheta(r, d) = TotalThetas(d).Data(r)
                End If
            Next
        Next
        unitedTotalTheta.setData(unitetheta)

        Strength_width = Integer.Parse(TextBox6.Text)
        sections = Integer.Parse(TextBox7.Text)
        ReDim Signal_Strength(sections)
        Dim center = Yran / 2 - 1

        For i = 0 To sections - 1
            Dim n = (11 + i).ToString
            Signal_Strength(i) = New MyData2D(FileMax, 1, Chart1, n + "次高調波の信号強度(X方向の積算より）", "XUV-IR delay", n + "次高調波")
        Next

        Dim Ranges(sections) As (Integer, Integer)
        Ranges(0) = (s11, f11)
        Ranges(1) = (s12, f12)
        Ranges(2) = (s13, f13)
        Ranges(3) = (s14, f14)
        Ranges(4) = (s15, f15)
        Dim signalMax = 0

        For sec = 0 To sections - 1 Step 1
            Dim unitesignals(FileMax, 1) As Double
            For d = 0 To FileMax - 1
                Dim summation = 0
                For x = Ranges(sec).Item1 To Ranges(sec).Item2 Step 1
                    summation += TotalYs(d).Data(x)
                Next
                If signalMax < summation Then
                    signalMax = summation
                End If
                unitesignals(d, 0) = summation
            Next
            Signal_Strength(sec).setData(unitesignals)
        Next


        ReDim Signal_Corrected(sections)
        Dim mode = "UP"
        Dim head As Double
        Dim head_t As Double
        Dim back2, back1, now, front1, front2 As Double
        Dim cnt, wid As Double
        For sec = 0 To sections - 1 Step 1
            Signal_Corrected(sec) = New List(Of (Double, Double))

            head = 0
            head_t = 0
            For x = 2 To FileMax - 1 - 2 Step 1
                back2 = Signal_Strength(sec).Data(x - 2, 0)
                back1 = Signal_Strength(sec).Data(x - 1, 0)
                now = Signal_Strength(sec).Data(x, 0)
                front1 = Signal_Strength(sec).Data(x + 1, 0)
                front2 = Signal_Strength(sec).Data(x + 2, 0)

                '案1 単純に大小関係で極値を探す
                If mode = "UP" And back2 < back1 And back1 < now And now < front1 And front1 < front2 Then

                    cnt = x - head
                    wid = 1.33 / cnt
                    For i = head To x - 1
                        Signal_Corrected(sec).Add((Signal_Strength(sec).Data(i, 0), head_t + wid * (i - head)))
                    Next
                    head = x
                    head_t += 1.33
                    mode = "DOWN"

                ElseIf mode = "DOWN" And back2 > back1 And back1 > now And now > front1 And front1 > front2 Then
                    cnt = x - head
                    wid = 1.33 / cnt
                    For i = head To x
                        Signal_Corrected(sec).Add((Signal_Strength(sec).Data(i, 0), head_t + wid * (i - head)))
                    Next
                    head = x + 1
                    head_t += 1.33
                    mode = "UP"
                End If

                '案2 前後の差の掛け算の符号で判断する
                'If (now - back1) * (front1 - now) < 0 And (now - back2) * (front2 - now) < 0 Then

                '    cnt = x - head
                '    wid = 1.33 / cnt
                '    For i = head To x - 1
                '        Signal_Corrected(sec).Add((Signal_Strength(sec).Data(i, 0), head_t + wid * (i - head)))
                '    Next
                '    head = x
                '    head_t += 1.33

                'End If
            Next
        Next

        'Dim title2 = CType("信号強度[a.u.]", String)
        'Chart2.Series.Clear()
        'Chart2.Series.Add(title2)
        'Chart2.Series(title2).ChartType = DataVisualization.Charting.SeriesChartType.Point
        'Chart2.Titles.Add("信号強度とXUV-IR Delayの関係")
        'Chart2.Series(title2).MarkerSize = 4
        'Chart2.Legends(0).MaximumAutoSize = 100

        'With Chart2.Titles.Item(0)
        '    .Position.Auto = True
        '    .Alignment = Drawing.ContentAlignment.BottomCenter
        '    .Docking = Docking.Bottom
        'End With

        'For sec = 0 To sections - 1 Step 1
        '    For x = 0 To FileMax - 1 Step 1
        '        Chart2.Series(title2).Points.AddXY(Signal_Corrected(sec)(x).Item2, Signal_Corrected(sec)(x).Item1)
        '    Next
        'Next
        With Chart1.Titles
            .Clear()
            .Add("n次高調波の信号強度")
        End With
        With Chart1.ChartAreas(0)
            .AxisY.Title = "信号強度"
            .AxisY.Maximum = signalMax * 1.1
        End With
        unitedTotalX.draw()
        unitedTotalY.draw()
        unitedTotalR.draw()
        unitedTotalTheta.draw()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Datas(ComboBox1.SelectedIndex).change()
            Datas_pols(ComboBox1.SelectedIndex).change()
            TotalXs(ComboBox1.SelectedIndex).change()
            TotalYs(ComboBox1.SelectedIndex).change()
            TotalRs(ComboBox1.SelectedIndex).change()
            TotalThetas(ComboBox1.SelectedIndex).change()


        Catch ex As System.ArgumentOutOfRangeException
            Exit Sub
        End Try


        PictureBox1.Refresh()
    End Sub



End Class

Public Class Vector2D
    Public x As Double
    Public y As Double
    Sub New(ByVal argx As Double, ByVal argy As Double)
        x = argx
        y = argy
    End Sub
End Class

Public Class MyData1D

    Public Direction As Integer '方向、0がx方向(横向き)、1がy方向(縦向き)
    Public ran As Integer
    Public Data() As Double
    Public Image As Image
    Public PictureBox As New PictureBox
    Public Size As New Size
    Sub New(ran As Integer, pic As PictureBox, dir As Integer)
        Me.ran = ran
        ReDim Data(ran)

        Me.PictureBox = pic
        If dir = 0 Then
            Me.Image = New Bitmap(ran, 5)
            Me.Size = New Size(ran, 5)
        ElseIf dir = 1 Then
            Me.Image = New Bitmap(5, ran)
            Me.Size = New Size(5, ran)
        End If





        Me.Direction = dir
    End Sub

    Sub draw()
        Dim g As Graphics = Graphics.FromImage(Me.Image)
        Dim max, min As Double

        max = Me.Data.Max
        min = Me.Data.Min

        Dim deg As New Integer
        For i = 0 To Me.ran Step 1
            deg = Int((Data(i) - min) / (max - min) * 255)
            Dim brush As New SolidBrush(Color.FromArgb(deg, deg, deg))
            If Me.Direction = 0 Then
                g.FillRectangle(brush, i, 0, 1, 5)
            ElseIf Me.Direction = 1 Then
                g.FillRectangle(brush, 0, i, 5, 1)
            End If
        Next
        Me.change()
    End Sub

    Sub change()
        Me.PictureBox.Size = Me.Size
        Me.PictureBox.Image = Me.Image
        Me.PictureBox.Refresh()

    End Sub
End Class
Public Class MyData2D
    Public Xran, Yran As Integer
    Public Data(,) As Double
    Public CenterX, CenterY As Integer
    Public Max, Min As Double
    Public Image As Image
    Public PictureBox As PictureBox
    Public Chart As Chart
    Public dataName As String
    Public labelX As String
    Public labelY As String
    Public Size As Size
    Public Maxima_indexes As List(Of Integer) = New List(Of Integer)
    Public Minimal_indexes As List(Of Integer) = New List(Of Integer)
    Private Type As String

    'ピークサーチ関連の定数
    Public Const RANGE = 2 '接線を求める際、前後何個のデータで最小二乗法するかという値
    Public Grads As List(Of Double) = New List(Of Double)
    Public Data_Correct_x As List(Of Double) = New List(Of Double)
    Public Data_Correct_y As List(Of Double) = New List(Of Double)

    Sub New(xran As Integer, yran As Integer, pic As PictureBox, nameData As String, nameX As String, nameY As String)
        Me.Type = "PictureBox"
        Me.Xran = xran
        Me.Yran = yran

        ReDim Data(xran, yran)

        Me.Image = New Bitmap(xran, yran)
        Me.PictureBox = pic
        Me.dataName = nameData
        Me.labelX = nameX
        Me.labelY = nameY
        Me.Size = New Size(xran, yran)
    End Sub

    Sub New(xran As Integer, yran As Integer, chart As Chart, nameData As String, nameX As String, nameY As String)
        Me.Type = "Chart"
        Me.Xran = xran
        Me.Yran = yran

        ReDim Data(xran, yran)

        Me.Image = New Bitmap(xran, yran)
        Me.Chart = chart
        Me.dataName = nameData
        Me.labelX = nameX
        Me.labelY = nameY
        Me.Size = New Size(xran, yran)

    End Sub

    Sub setData(ByRef data(,) As Double)
        Me.Data = data
        'maxとminを探す,maxは中心にあると仮定し、同時に中心座標を割り出す
        Dim dmax, dmin As Double
        Dim center_x, center_y As New Int32
        dmax = data(0, 0)
        dmin = data(0, 0)
        For i = 0 To Xran Step 1
            For j = 0 To Yran Step 1
                If data(i, j) > dmax Then
                    dmax = data(i, j)
                    center_x = i
                    center_y = j
                End If
                If data(i, j) < dmin Then
                    dmin = data(i, j)
                End If
            Next
        Next
        Me.CenterX = center_x
        Me.CenterY = center_y
        Me.Max = dmax
        Me.Min = dmin
    End Sub
    Sub draw()
        If Me.Type = "PictureBox" Then
            Dim g As Graphics = Graphics.FromImage(Me.Image)
            Dim deg As New Integer
            For y = 0 To Yran Step 1
                For x = 0 To Xran Step 1
                    If Me.Max - Me.Min = 0 Then
                        deg = 0
                        Console.WriteLine(Me.dataName + "のデータはすべて0です")
                    Else
                        deg = Int(Data(x, y) / (Me.Max - Me.Min) * 255)
                        If deg < 0 Then
                            deg = 0
                        End If
                    End If

                    Dim brush As New SolidBrush(Color.FromArgb(deg, deg, deg))
                    g.FillRectangle(brush, x, y, 1, 1)
                    brush.Dispose()
                Next
            Next
            '座標軸と中心の点を引いてみる
            Dim br2 As New SolidBrush(Color.FromArgb(255, 0, 0))
            ' g.FillRectangle(br2, Convert.ToInt32(Me.Xran / 2), 0, 1, Me.Yran)
            'g.FillRectangle(br2, 0, Convert.ToInt32(Me.Yran / 2), Me.Xran, 1)
            g.FillRectangle(br2, Me.CenterX, Me.CenterY, 1, 1)

            Me.change()
        Else
            Console.WriteLine("このデータはPictureBoxに紐付けられていません。draw()を実行することはできません。")
        End If
    End Sub

    Sub plot()
        If Me.Type = "Chart" Then
            Me.Chart.Series.Add(CType(Me.labelY, String))
            Me.Chart.Series(CType(Me.labelY, String)).ChartType = DataVisualization.Charting.SeriesChartType.Point
            Me.Chart.Titles.Add(Me.dataName)
            Me.Chart.Series(CType(Me.labelY, String)).MarkerSize = 4
            Me.Chart.Legends(0).MaximumAutoSize = 100

            With Me.Chart.Titles.Item(0)
                .Position.Auto = True
                .Alignment = Drawing.ContentAlignment.BottomCenter
                .Docking = Docking.Bottom
            End With

            For y = 0 To Me.Yran - 1 Step 1
                For x = 0 To Me.Xran - 1 Step 1
                    Me.Chart.Series(CType(Me.labelY, String)).Points.AddXY(x, Me.Data(x, y))
                Next
            Next


            With Me.Chart.ChartAreas(0)
                With .AxisX
                    .Minimum = 0
                    .Maximum = Me.Xran * 1.1
                    .Interval = 1 * 10 ^ (-15)
                    .Title = Me.labelX

                End With

                With .AxisY
                    .Maximum = Me.Max * 1.1
                    .Title = Me.labelY
                End With
            End With

        Else
            Console.WriteLine("このデータはChartに紐付けられていません。plot()を実行することはできません。")
        End If
    End Sub

    Sub change()
        Me.PictureBox.Size = Me.Size
        Me.PictureBox.Image = Me.Image
        Me.PictureBox.Refresh()
    End Sub

    Sub output(ByVal path As String)
        'Using writer = New StreamWriter("")
    End Sub

    Sub peakSerchY(ByVal Y As Integer)
        Console.WriteLine(Me.dataName)
        For i = RANGE To Me.Yran - 1 - RANGE Step 1
            If Me.Data(i - 2, Y) < Me.Data(i - 1, Y) Then

            End If
        Next



        Console.WriteLine(Me.Maxima_indexes.Count)
        Console.WriteLine(Me.Minimal_indexes.Count)

        Dim head As Integer
        Dim head_time As Double
        Dim wid As Double
        head = 0

        For current = 0 To Me.Xran - 1 Step 1
            If Me.Maxima_indexes.IndexOf(current) <> -1 Then
                wid = 1.33 / (current - head + 1)
                For j = head To current Step 1
                    Me.Data_Correct_x.Add((head_time + wid * j))
                    Me.Data_Correct_y.Add(Me.Data(j, Y))
                Next
                head = current + 1
                head_time += 1.33
            ElseIf Me.Minimal_indexes.IndexOf(current) <> -1 Then
                wid = 1.33 / (current - head + 1)
                For j = head To current Step 1
                    Me.Data_Correct_x.Add((head_time + wid * j))
                    Me.Data_Correct_y.Add(Me.Data(j, Y))
                Next
                head = current + 1
                head_time += 1.33
            Else

            End If
        Next
        Console.WriteLine(Me.Data_Correct_x.Count)
        Console.WriteLine(Me.Data_Correct_y.Count)
    End Sub

    Sub plot_correct()
        If Me.Type = "Chart" Then
            Me.Chart.Series.Add(CType(Me.labelY, String))
            Me.Chart.Series(CType(Me.labelY, String)).ChartType = DataVisualization.Charting.SeriesChartType.Point
            Me.Chart.Titles.Add(Me.dataName)
            Me.Chart.Series(CType(Me.labelY, String)).MarkerSize = 4
            Me.Chart.Legends(0).MaximumAutoSize = 100

            With Me.Chart.Titles.Item(0)
                .Position.Auto = True
                .Alignment = Drawing.ContentAlignment.BottomCenter
                .Docking = Docking.Bottom
            End With

            For i = 0 To Me.Data_Correct_x.Count - 1 Step 1
                Me.Chart.Series(CType(Me.labelY, String)).Points.AddXY(Me.Data_Correct_x(i), Me.Data_Correct_y(i))
            Next


            With Me.Chart.ChartAreas(0)
                With .AxisX
                    .Minimum = 0
                    .Maximum = Me.Xran * 1.1
                    .Interval = 1 * 10 ^ (-15)
                    .Title = Me.labelX

                End With

                With .AxisY
                    .Maximum = Me.Max * 1.1
                    .Title = Me.labelY
                End With
            End With

        Else
            Console.WriteLine("このデータはChartに紐付けられていません。plot()を実行することはできません。")
        End If

    End Sub

End Class
