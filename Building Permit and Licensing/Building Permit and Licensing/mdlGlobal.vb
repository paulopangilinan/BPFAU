Imports System.Data.Odbc
Imports DevComponents.DotNetBar
Imports System.Drawing
Module mdlGlobal
#Region "Database Variables"
    Public server As String
    Public database As String
    Public sqluser As String
    Public sqlpassword As String
    Public sqlport As String
    Public Const registryPath As String = "HKEY_CURRENT_USER\Software\BPFAU"
    Public Const registrySubkey As String = "Software\BPFAU"
#End Region

    Public loggedUserName As String
    Public userID As String
    Public sqlCon As New Odbc.OdbcConnection
    Public sqlDA As New Odbc.OdbcDataAdapter
    Public sqlDS As New DataSet
    Dim ctr As Integer = 0
    Public credential As String, userLoginID As Integer
    Public frmMain1 As New formMain
    Public frmLogin1 As New formLogin
    Public displayname As String, designation As String, userImage As String, usertype As String
    Public normalHeight As Integer, normalWidth As Integer, maxHeight As Integer, maxWidth As Integer
    Public itemType As String

    Public Sub establishSQLConnection(ByVal connectionString As String)
        sqlCon = New Odbc.OdbcConnection(connectionString)
        Try
            sqlCon.Open()
        Catch ex As Exception
            MessageBoxEx.Show("MySQL Connection Failed.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Function testSQLConnection(ByVal connectionString As String) As Boolean
        sqlCon = New Odbc.OdbcConnection(connectionString)
        Try
            sqlCon.Open()
            If sqlCon.State = 1 Then
                sqlCon.Close()
                sqlCon.Dispose()
                GC.Collect()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function mySQLConnectionString(ByVal sqlserver As String, _
                                            ByVal sqlport As String, _
                                            ByVal sqldatabase As String, _
                                            ByVal sqluid As String, _
                                            ByVal sqlpass As String) As String

        Return "DRIVER={MySQL ODBC 5.1 Driver};SERVER=" & sqlserver & _
                ";PORT=" & sqlport & ";DATABASE=" & sqldatabase & ";UID=" & sqluid & ";PWD=" & sqlpass & ";OPTION=3"
    End Function

    Public Function executeQuery(ByVal query As String, ByVal tableName As String) As DataTable
        sqlDA = New Odbc.OdbcDataAdapter(query, sqlCon)
        sqlDS = New DataSet
        sqlDA.Fill(sqlDS, tableName)
        Return sqlDS.Tables(tableName)
    End Function


    Public Sub createConnectionRegistry(ByVal sqlserver As String, _
                                            ByVal sqlport As String, _
                                            ByVal sqldatabase As String, _
                                            ByVal sqluid As String, _
                                            ByVal sqlpass As String)
        With My.Computer.Registry
            .CurrentUser.CreateSubKey(registrySubkey)
            .SetValue(registryPath, My.Resources.DatabaseServerRegistryPath, sqlserver.Trim)
            .SetValue(registryPath, My.Resources.DatabasePortRegistryPath, sqlserver.Trim)
            .SetValue(registryPath, My.Resources.DatabaseNameRegistryPath, sqldatabase.Trim)
            .SetValue(registryPath, My.Resources.DatabaseUserRegistryPath, sqluid.Trim)
            .SetValue(registryPath, My.Resources.DatabasePasswordRegistryPath, sqlpass.Trim)
        End With
    End Sub
    Public Sub getMySQLCredentials()
        With My.Computer.Registry
            .CurrentUser.CreateSubKey(registrySubkey)
            server = .GetValue(registryPath, My.Resources.DatabaseServerRegistryPath, Nothing)
            sqlport = .GetValue(registryPath, My.Resources.DatabasePortRegistryPath, Nothing)
            database = .GetValue(registryPath, My.Resources.DatabaseNameRegistryPath, Nothing)
            sqluser = .GetValue(registryPath, My.Resources.DatabaseUserRegistryPath, Nothing)
            sqlpassword = .GetValue(registryPath, My.Resources.DatabasePasswordRegistryPath, Nothing)
        End With
    End Sub

    Public Sub clearAllInput(ByRef ctrl As Control)
        For Each ctrlX As Control In ctrl.Controls
            If TypeOf ctrlX Is DevComponents.DotNetBar.Controls.TextBoxX Then
                ctrlX.Text = String.Empty
            End If
        Next
    End Sub

    Public Function initializeQueryEntry(ByVal phrase As String, ByVal isForFiltering As Boolean) As String
        If isForFiltering = True Then
            If phrase.IndexOf("\") Then
                phrase = Replace(phrase, "\", "\\\")
                phrase = Replace(phrase, "'", "''")
            Else
                phrase = Replace(phrase, "\", "\\")
                phrase = Replace(phrase, "'", "''")
            End If
        Else
            phrase = Replace(phrase, "\", "\\")
            phrase = Replace(phrase, "'", "''")
        End If
        Return phrase
    End Function

    Public Sub initButtons(ByRef control As Control)
        For Each ctrl As Control In control.Controls
            Call initButtons(ctrl)
            If TypeOf ctrl Is Button Or TypeOf ctrl Is ButtonX Then
                ctrl.BackColor = Color.FromArgb(52, 69, 99)
                ctrl.ForeColor = Color.White
                ctrl.Refresh()
                If ctrl.Tag = "Navy Encode" Then
                    If usertype = "1" Then
                        ctrl.Enabled = False
                    End If
                End If
                If ctrl.Tag = "plumberonly" Then
                    If usertype = "2" Then
                        ctrl.Visible = True
                    Else
                        ctrl.Visible = False
                    End If
                End If
                If ctrl.Tag = "PNBonly" Then
                    If usertype = "2" Or usertype = "1" Then
                        ctrl.Visible = True
                    Else
                        ctrl.Visible = False
                    End If
                End If
            End If
            If TypeOf ctrl Is Label Or TypeOf ctrl Is LabelX Then
                If ctrl.Tag = "White" Then
                    ctrl.ForeColor = Color.White
                ElseIf ctrl.Tag = "Navy" Then
                    ctrl.ForeColor = Color.FromArgb(52, 69, 99)
                Else
                    ctrl.ForeColor = Color.FromArgb(64, 64, 64)
                End If
            End If
        Next
    End Sub

    Public Sub updateAdminImageDisplay(ByRef pictureDis As PictureBox, ByVal adminID As String)
        Dim quer As String = "SELECT UserImage" & " from tblUsers WHERE UserID = '" & initializeQueryEntry(adminID, True) & "'"
        Dim dtAdminPic As DataTable = executeQuery(quer, "tblAdminPhoto")
        If dtAdminPic.Rows.Count > 0 Then
            If IO.File.Exists(dtAdminPic.Rows(dtAdminPic.Rows.Count - 1)(0).ToString) Then
                pictureDis.Image = Image.FromFile(dtAdminPic.Rows(dtAdminPic.Rows.Count - 1)(0).ToString)
            Else
                pictureDis.Image = My.Resources.DefaultUser
            End If
        End If
    End Sub

    Public Sub CleanUpFields(ByRef parent As Control, Optional ByVal operation As String = "Add")
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                txt.ReadOnly = False
                If operation = "Add" Then
                    txt.Text = String.Empty
                End If
            End If
        Next
    End Sub

    Public Sub lockClearFields(ByRef parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                txt.ReadOnly = True
                txt.Text = String.Empty
            End If
        Next
    End Sub

    Public Sub logtrail(ByVal userID As String, ByRef buttonClick As String, ByVal form As String, ByVal value As String)
        Dim transaction As String = form & " - "
        transaction &= buttonClick.Replace("&", "") & " - "
        transaction &= value
        Dim q As String = "INSERT INTO tblTrail VALUES('" & userID & "','" & initializeQueryEntry(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), False) & "','" & initializeQueryEntry(transaction, False) & "')"
        executeQuery(q, "insertTrail" & DateTime.Now.ToString("ddmmss"))
    End Sub
End Module
