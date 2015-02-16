Imports System.IO
Imports System.Runtime.InteropServices
Imports System
Imports Microsoft.Win32
Imports DevComponents.DotNetBar
Imports System.Drawing.Drawing2D
Public Class formLogin
    <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    End Function
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Private userIDLogin As Integer
    Private userList(-1) As String
    Private answer As String

    Private Sub Form_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelLogin.MouseDown, panelMySQL.MouseDown, Label1.MouseDown, Label2.MouseDown, Label10.MouseDown, PanelEx3.MouseDown, PanelEx7.MouseDown, Label3.MouseDown

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub Form_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelLogin.MouseUp, panelMySQL.MouseUp, Label1.MouseUp, Label2.MouseUp, Label10.MouseUp, PanelEx3.MouseUp, PanelEx7.MouseUp, Label3.MouseUp

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Form_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelLogin.MouseMove, panelMySQL.MouseMove, Label1.MouseMove, Label2.MouseMove, Label10.MouseMove, PanelEx3.MouseMove, PanelEx7.MouseMove, Label3.MouseMove

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub

    Private Sub formLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.BackColor = System.Drawing.ColorTranslator.FromHtml("#D5DEE7")
        Call initButtons(Me)
        Call getMySQLCredentials()
        Me.StartPosition = FormStartPosition.CenterScreen
        slideMySQL.IsOpen = False
        slideLoginError.IsOpen = False
        slideNewUser.IsOpen = False
        slideForgot.IsOpen = False

        If testSQLConnection(mySQLConnectionString(server, sqlport, database, sqluser, sqlpassword)) Then
            Call establishSQLConnection(mySQLConnectionString(server, sqlport, database, sqluser, sqlpassword))
            picLogin.Image = My.Resources.DefaultUser
            Call loadQuestions()
        Else
            MessageBoxEx.Show("Cannot establish connection to the database." & vbNewLine & "Please configure your database connectivity.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Sub centralizedPanels()
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.SlidePanel Then
                Dim slide As New DevComponents.DotNetBar.Controls.SlidePanel
                ctrl = slide
                ctrl.Location = New Point(((Me.Width - ctrl.Width) / 2) - 10, ((Me.Height - ctrl.Height) / 2) - 10)
            End If
        Next
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        slideNewUser.IsOpen = True
    End Sub

    Sub loadQuestions()
        cboQuestion.Items.Clear()
        cboForgotQUestion.Items.Clear()
        Dim q As String = "SELECT Question FROM tblquestions ORDER BY ID ASC"
        Dim dtq As DataTable = executeQuery(q, "getQuestions")
        If dtq.Rows.Count > 0 Then
            For i As Integer = 0 To dtq.Rows.Count - 1
                cboQuestion.Items.Add(dtq.Rows(i)(0).ToString)
                cboForgotQUestion.Items.Add(dtq.Rows(i)(0).ToString)
            Next
        End If
    End Sub
    'Sub loadAdminAccounts()
    '    Dim query As String = "SELECT UserID, CONCAT(FirstName,' ',LastName), UserDescription, UserImage FROM tblUsers"
    '    Dim dataAccounts As DataTable = executeQuery(query, "tblUsers")
    '    If dataAccounts.Rows.Count > 0 Then
    '        cboUsernames.Items.Clear()
    '        For userCount As Integer = 0 To dataAccounts.Rows.Count - 1
    '            ReDim Preserve userList(userList.Length)
    '            userList(userList.Length - 1) = dataAccounts.Rows(userCount)(0).ToString
    '            cboUsernames.Items.Add(dataAccounts.Rows(userCount)(1).ToString)
    '        Next
    '        cboUsernames.SelectedIndex = 0
    '    End If
    '    dataAccounts.Dispose()
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If testSQLConnection(mySQLConnectionString(Trim(txtDBServer.Text), Trim(txtDBPort.Text), Trim(txtDBdatabasename.Text), _
                                                     Trim(txtDBName.Text), Trim(txtDBPassword.Text))) Then
            Call createConnectionRegistry(Trim(txtDBServer.Text), Trim(txtDBPort.Text), Trim(txtDBdatabasename.Text), _
                                                     Trim(txtDBName.Text), Trim(txtDBPassword.Text))
            Call getMySQLCredentials()
            Call establishSQLConnection(mySQLConnectionString(server, sqlport, database, sqluser, sqlpassword))
            MessageBoxEx.Show("Connected to server : ." & server, My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            slideMySQL.IsOpen = False
            'Call loadAdminAccounts()
        Else
            MessageBoxEx.Show("Cannot establish connection to the database." & vbNewLine & "Please check entered configuration keys.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Call clearAllInput(panelDB)
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        slideMySQL.IsOpen = False
    End Sub

    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click
        Try
            If txtUsername.Text.Trim = My.Resources.programmerLogin And txtUserPassword.Text.Trim = My.Resources.programmerPassword Then
                slideMySQL.IsOpen = True
            Else
                If checkLoginCredentials(txtUsername.Text, txtUserPassword.Text) Then
                    Call logtrail(userIDLogin, btnLogin.Text, "Accounts", btnLogin.Text)
                    userID = userIDLogin
                    frmMain1 = New formMain
                    frmMain1.lblLoggedName.Text = displayname
                    frmMain1.lblLoggedDesign.Text = designation
                    If IO.File.Exists(userImage) Then
                        frmMain1.picLoggedImage.Image = Image.FromFile(userImage)
                        frmMain1.picAccount.Image = Image.FromFile(userImage)
                    Else
                        frmMain1.picLoggedImage.Image = My.Resources.DefaultUser
                        frmMain1.picAccount.Image = My.Resources.DefaultUser
                    End If

                    frmMain1.Show()
                    Me.Hide()
                Else
                    slideLoginError.IsOpen = True
                    btnError.Focus()
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Function checkLoginCredentials(ByVal username As String, ByVal password As String, Optional ByVal hash As Boolean = True) As Boolean
        Dim query As String = "SELECT UserID, UserType, UserDescription, Username, CONCAT(FirstName,' ',LastName), MiddleName, UserPwd, UserImage FROM tblUsers WHERE " & _
                        "UserName = '" & initializeQueryEntry(username, True) & "' " & _
                        "AND UserPwd = " & IIf(hash = True, "MD5('" & initializeQueryEntry(password, True) & "')", "'" & initializeQueryEntry(password, True) & "'")
        Dim dtLogin As DataTable = executeQuery(query, "tblUsers")
        If dtLogin.Rows.Count > 0 Then
            userIDLogin = dtLogin.Rows(dtLogin.Rows.Count - 1)(0).ToString
            displayname = dtLogin.Rows(dtLogin.Rows.Count - 1)(4).ToString
            designation = dtLogin.Rows(dtLogin.Rows.Count - 1)(2).ToString
            userImage = dtLogin.Rows(dtLogin.Rows.Count - 1)(7).ToString
            loggedUserName = dtLogin.Rows(dtLogin.Rows.Count - 1)(4).ToString
            usertype = dtLogin.Rows(dtLogin.Rows.Count - 1)(1).ToString
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnEnd_Click(sender As System.Object, e As System.EventArgs) Handles btnEnd.Click
        Application.Exit()
    End Sub

    Private Sub btnError_Click(sender As System.Object, e As System.EventArgs) Handles btnError.Click
        slideLoginError.IsOpen = False
        Call clearAllInput(panelLogin)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        slideNewUser.IsOpen = False
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If txtRegUser.Text.Trim = "" Then
            MessageBoxEx.Show("Username is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRegUser.Focus()
            Exit Sub
        End If
        If txtRegF.Text.Trim = "" Then
            MessageBoxEx.Show("First name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRegF.Focus()
            Exit Sub
        End If
        If txtRegL.Text.Trim = "" Then
            MessageBoxEx.Show("Last name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRegL.Focus()
            Exit Sub
        End If
        If cboDesignation.SelectedIndex = -1 Then
            MessageBoxEx.Show("Designation is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboDesignation.Focus()
            Exit Sub
        End If
        If txtRegPass.Text = "" Or (txtRegPass.Text = "" And txtRegRePass.Text = "") Then
            MessageBoxEx.Show("Password is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRegPass.Focus()
            Exit Sub
        End If
        If (txtRegPass.Text <> "" And txtRegPass.Text <> txtRegRePass.Text) Or _
            (txtRegRePass.Text <> "" And txtRegRePass.Text <> txtRegPass.Text) Then
            MessageBoxEx.Show("Passwords not matched.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRegPass.Focus()
            Exit Sub
        End If
        If cboQuestion.SelectedIndex = -1 Then
            MessageBoxEx.Show("Security question is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboQuestion.Focus()
            Exit Sub
        End If
        If TextBoxX2.Text.Trim = "" Then
            MessageBoxEx.Show("Security answer is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX2.Focus()
            Exit Sub
        End If
        Call saveNewAccount()
        MessageBoxEx.Show("New account successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        ReDim userList(-1)
        'Call loadAdminAccounts()
        Call logtrail(userIDLogin, "New User", "Accounts", txtRegUser.Text.Trim)
        slideNewUser.IsOpen = False
    End Sub

    Sub saveNewAccount()
        Dim q As String = "INSERT INTO tblUsers(UserType, UserDescription, Username, FirstName, LastName, MiddleName, UserPwd,Question,Answer) " & _
            "VALUES('" & cboDesignation.SelectedIndex + 1 & "','" & cboDesignation.SelectedItem.ToString() & "','" & initializeQueryEntry(txtRegUser.Text.Trim, False) & _
            "','" & initializeQueryEntry(txtRegF.Text.Trim, False) & "','" & initializeQueryEntry(txtRegL.Text.Trim, False) & _
            "','" & initializeQueryEntry(txtRegM.Text.Trim, False) & "',MD5('" & initializeQueryEntry(txtRegPass.Text.Trim, False) & "')," & _
            "'" & initializeQueryEntry(cboQuestion.SelectedIndex + 1, False) & "','" & initializeQueryEntry(TextBoxX2.Text.Trim, False) & "')"
        Dim dtLoginSave As DataTable = executeQuery(q, "tblUserSave")

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        Call loadQuestions()
        If txtUsername.Text.Trim = "" Then
            MessageBoxEx.Show("Please enter your username to proceed.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            Exit Sub
        End If
        Call getSecurityQuestion()
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        slideForgot.IsOpen = False
    End Sub

    Sub getSecurityQuestion()
        Dim q As String = "SELECT Question, Answer FROM tblUsers WHERE Username = '" & initializeQueryEntry(txtUsername.Text.Trim, True) & "'"
        Dim dt As DataTable = executeQuery(q, "getSecurity")
        If dt.Rows.Count > 0 Then
            cboForgotQUestion.SelectedIndex = CInt(dt.Rows(0)(0).ToString) - 1
            answer = dt.Rows(0)(1).ToString
            slideForgot.IsOpen = True
        Else
            MessageBoxEx.Show("User not found. Please check your username.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If TextBoxX6.Text.Trim = "" Then
            MessageBoxEx.Show("Security answer is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX6.Focus()
            Exit Sub
        End If
        If TextBoxX6.Text.Trim.ToLower = answer.ToLower Then
            panelNewPassword.Visible = True
            TextBoxX6.Text = ""
            Call logtrail(userIDLogin, "Forgot Password", "Accounts", "***")
        Else
            MessageBoxEx.Show("Incorrect answer to security question.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX6.Text = ""
            TextBoxX6.Focus()
        End If
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        If TextBoxX3.Text = "" Or (TextBoxX3.Text = "" And TextBoxX4.Text = "") Then
            MessageBoxEx.Show("Password is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX3.Focus()
            Exit Sub
        End If
        If (TextBoxX3.Text <> "" And TextBoxX3.Text <> TextBoxX4.Text) Or _
            (txtRegRePass.Text <> "" And TextBoxX4.Text <> TextBoxX3.Text) Then
            MessageBoxEx.Show("Passwords not matched.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX3.Focus()
            Exit Sub
        End If
        Dim q As String = "UPDATE tblUsers SET UserPwd = MD5('" & initializeQueryEntry(TextBoxX3.Text.Trim, False) & "') WHERE Username = '" & initializeQueryEntry(txtUsername.Text.Trim, True) & "'"
        Dim dtSavePass As DataTable = executeQuery(q, "tblSavePass")
        MessageBoxEx.Show("Password successfully changed.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Call logtrail(userIDLogin, "Reset Password", "Accounts", "***")
        panelNewPassword.Visible = False
        slideForgot.IsOpen = False
    End Sub

    Private Sub PanelEx5_Click(sender As System.Object, e As System.EventArgs) Handles PanelEx5.Click

    End Sub
End Class