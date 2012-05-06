Imports Xwt

Public Class ListViewDataTable
    Inherits Xwt.ListView

    Private _datasource As DataTable
    Public Property Value As DataTable
        Get
            Return _datasource
        End Get
        Set(value As DataTable)
            _datasource = value

            Dim fields(_datasource.Columns.Count - 1) As Xwt.DataField

            For i As Integer = 0 To _datasource.Columns.Count - 1

                Dim t As Type = _datasource.Columns(i).DataType
                If t.ToString = "System.Int64" Then
                    fields(i) = New DataField(Of Integer)
                ElseIf t.ToString = "System.String" Then
                    fields(i) = New DataField(Of String)
                Else
                    Throw New Exception("Unknown type")
                End If

            Next

            Dim store As New ListStore(fields)

            Me.DataSource = store

            ' Add the columns
            For i As Integer = 0 To _datasource.Columns.Count - 1
                Me.Columns.Add(_datasource.Columns(i).ColumnName, fields(i))
            Next

            ' Add the row data
            For i As Integer = 0 To _datasource.Rows.Count - 1
                Dim r As Integer = store.AddRow()

                ' Set the current row column values
                For x As Integer = 0 To _datasource.Columns.Count - 1

                    Dim t As String = fields(x).GetType.ToString
                    If t = "Xwt.DataField`1[System.Int64]" Then
                        store.SetValue(r, DirectCast(fields(x), DataField(Of Integer)), CInt(_datasource.Rows(i)(x)))
                    ElseIf t = "Xwt.DataField`1[System.String]" Then
                        store.SetValue(r, DirectCast(fields(x), DataField(Of String)), _datasource.Rows(i)(x).ToString)
                    End If

                Next

            Next
        End Set
    End Property

End Class
