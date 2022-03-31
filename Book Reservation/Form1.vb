Public Class Form1
    Public Sub Fill_Products()
        Dim dt As New DataTable
        dt = ExecuteQuery("SELECT * FROM Book Reservation")
        Try
            If dt.Rows.Count > 0 Then
                For ctr = 0 To dt.Rows.Count - 1
                    Dim item As New ListViewItem

                    item.Text = dt.Rows(ctr)("When")
                    item.SubItems.Add(dt.Rows(ctr)("What Time"))
                    item.SubItems.Add(dt.Rows(ctr)("Number of guest"))
                    item.SubItems.Add(dt.Rows(ctr)("What Occasion"))
                    item.SubItems.Add(dt.Rows(ctr)("Special Request"))
                    LVPL.Items.Add(item)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub ViewSelectedLVPLProducts(ByVal ReservationID As String)
        Dim dt As New DataTable
        Dim ctr As Integer
        dt = ExecuteQuery("SELECT * FROM  Book Reservation WHERE When = '" & "'")
        If dt.Rows.Count > 0 Then
            txtWhen.Text = (dt.Rows(ctr)("When"))
            txtWhat.Text = (dt.Rows(ctr)("What Time"))
            txtNg.Text = (dt.Rows(ctr)("Number of guest"))
            txtWO.Text = (dt.Rows(ctr)("What Occasion"))
            txtSQ.Text = (dt.Rows(ctr)("Special Request"))
        End If
    End Sub

    Public Sub Add()
        Dim dt As New DataTable

        If txtWhen.Text = Nothing Or txtWhat.Text = Nothing Or txtNg.Text = Nothing Or txtWO.Text = Nothing Or txtSQ.Text = Nothing Then
            MessageBox.Show("Please fill all the fields!")

        Else
            dt = ExecuteQuery("INSERT INTO Book Reservation VALUES('" & txtWhen.Text.Trim & " ',' " &
                                        txtWhat.Text.Trim & "',' " &
                                        txtNg.Text.Trim & "',' " &
                                        txtWO.Text.Trim & "',' " &
                                        txtSQ.Text.Trim & "')")

            LVPL.Items.Clear()
            Call Fill_Products()

        End If
    End Sub

    Public Sub Edit()
        Try
            Dim dt As New DataTable
            dt = ExecuteQuery("UPDATE  Book Reservation SET When = '" &
txtWhen.Text.Trim & "' , What Time = '" & txtWhat.Text.Trim & "' , Number of Guest = 
'" & txtNg.Text.Trim & "' What Occasion = '" & txtWO.Text & "' Special Request = '" & txtSQ.Text)

            LVPL.Items.Clear()
            Call Fill_Products()
            MessageBox.Show("Product Updated")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Delete()

        Dim dt As New DataTable
        dt = ExecuteQuery("DELETE FROM  Book Reservation WHEN = '" & txtWhen.Text & "'")

    End Sub
    Private Sub LVPL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVPL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            For Each item As ListViewItem In LVPL.SelectedItems
                Dim first, last As Integer
                Dim str As String = item.SubItems(0).ToString
                first = str.IndexOf("{")
                last = str.LastIndexOf("}")
                str = item.SubItems(0).ToString.Substring(first + 1, last - first - 1)
                Call ViewSelectedLVPLProducts(str)
            Next


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        Call Add()
        Call Edit()
        Call Delete()

    End Sub
End Class
