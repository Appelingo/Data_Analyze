Public Class Form1
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
        TextBox1.Text = PathOP

        Dim tn As String
        Dim ftn As String
        Dim fn As String

        Dim Xran = 490
        Dim Yran = 490
        Dim FileMax = 10
        Dim pdata(Xran, Yran) As Double
        Dim sute, suteA As String
        Dim suteD As Double
        tn = TextBox1.Text

        '##### file name ####
        For ff = 1 To FileMax '### for a file

            ftn = ff.ToString
            fn = tn + ftn + ".dat" '#### file name "

            '#### open fine ####
            FileSystem.FileOpen(7, fn, OpenMode.Input)
            For i = 1 To Xran Step 1 '## X number '##

                For j = 1 To Yran Step 1 '### Y number  ##
                    Input(7, sute)
                    suteD = Double.Parse(sute)
                    Console.WriteLine(suteD)
                    pdata(i, j) = suteD '### pdata ##

                Next
                Input(7, suteA)

            Next

            FileSystem.FileClose(7)

        Next
        Console.WriteLine(pdata(490, 490))

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class
