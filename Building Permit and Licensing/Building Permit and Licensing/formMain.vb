Imports System.IO
Imports System.Runtime.InteropServices
Imports System
Imports Microsoft.Win32
Imports DevComponents.DotNetBar
Imports System.Drawing.Drawing2D
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class formMain
    Public remark_A As CrystalDecisions.Shared.ParameterDiscreteValue
    Public remark_B As CrystalDecisions.Shared.ParameterValues
    <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    End Function
    Private IsFormBeingDragged As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    Private imageUpload As String, profilePassword As String
    Private userNameEdit As String
    Private brgyAssessment(-1) As String, subdAssessment(-1) As String, classAssessment(-1) As String
    Public additionalAssessment As Boolean = False
    Public searchmode As String

#Region "Form Events"


    Private Sub Form_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelActionBar.MouseDown

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub

    Private Sub Form_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelActionBar.MouseUp

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub

    Private Sub Form_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelActionBar.MouseMove

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub

    Private Sub formMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call initButtons(Me)
        Me.BackColor = Color.Silver
        picBtnMax.Image = My.Resources.Restored_3

        picBtnMax.Tag = "Maximized"
        panelAccount.Visible = False
        slideBuilding.IsOpen = False
        slideBarangays.IsOpen = False
        slideSubdivision.IsOpen = False
        slideAssessor.IsOpen = False
        slideFees.IsOpen = False
        slideProfile.IsOpen = False
        slideAssessment.IsOpen = False
        slideSummary.IsOpen = False
        slideOccupancy.IsOpen = False
        slidePayment.IsOpen = False
        slideTrail.IsOpen = False
        Call voidOldAssessments(7)
        Dim _blankContextMenu As New ContextMenu()
        txtPaymentPaid.ContextMenu = _blankContextMenu
        txtPaymentOR.ContextMenu = _blankContextMenu
    End Sub

#End Region

#Region "Minimize and Restore Button/Icluding Profile Icon Event"

    Private Sub picBtnMini_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMini.MouseHover
        picBtnMini.BackColor = Color.Azure
    End Sub

    Private Sub picBtnMini_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMini.MouseLeave
        picBtnMini.BackColor = Color.Transparent
    End Sub

    Private Sub picBtnMax_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMax.MouseHover
        picBtnMax.BackColor = Color.Azure
    End Sub

    Private Sub picBtnMax_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMax.MouseLeave
        picBtnMax.BackColor = Color.Transparent
    End Sub


    Private Sub picBtnMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMax.Click
        If picBtnMax.Tag = "Maximized" Then
            Me.WindowState = FormWindowState.Normal
            picBtnMax.Image = My.Resources.Maximized_3
            picBtnMax.Tag = "Restored"
        Else
            Me.WindowState = FormWindowState.Maximized
            picBtnMax.Image = My.Resources.Restored_3
            picBtnMax.Tag = "Maximized"
        End If
    End Sub

    Private Sub picBtnMini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBtnMini.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub picAccount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAccount.Click
        panelAccount.Location = New Point(getSLidePoint(Me.Width, panelAccount.Width), 55) ' Reposition Popup See function getSLidePoint
        If panelAccount.Visible = True Then
            panelAccount.Visible = False
        Else
            panelAccount.Visible = True
        End If
    End Sub

    'Function to reposition popup upon clicking the top profile picture : Adjusts depending on monitor size
    Function getSLidePoint(ByVal parentWidth As Integer, ByVal ctrlWidth As Integer) As Integer
        Dim pointX As Integer
        pointX = parentWidth - (ctrlWidth + 20)
        Return pointX
    End Function

#End Region

#Region "Sidebar selection"


    Private Sub picPanelAssessment_Click(sender As System.Object, e As System.EventArgs) Handles picPanelAssessment.Click, picAssessment.Click
        If expAssessment.Expanded = True Then
            expAssessment.Expanded = False
        Else
            expAssessment.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelOccupancy_Click(sender As System.Object, e As System.EventArgs) Handles picPanelOccupancy.Click, picOccupancy.Click
        If expOccupancy.Expanded = True Then
            expOccupancy.Expanded = False
        Else
            expOccupancy.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelBuilding_Click(sender As System.Object, e As System.EventArgs) Handles picPanelBuilding.Click, picBuilding.Click
        If expBuilding.Expanded = True Then
            expBuilding.Expanded = False
        Else
            expBuilding.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelBarangay_Click(sender As System.Object, e As System.EventArgs) Handles picPanelBarangay.Click, picBrgy.Click
        If expBarangay.Expanded = True Then
            expBarangay.Expanded = False
        Else
            expBarangay.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelSubdivision_Click(sender As System.Object, e As System.EventArgs) Handles picPanelSubdivision.Click, picSubd.Click
        If expSubdivision.Expanded = True Then
            expSubdivision.Expanded = False
        Else
            expSubdivision.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelAssess_Click(sender As System.Object, e As System.EventArgs) Handles picPanelAssess.Click, picAssess.Click
        If expAssess.Expanded = True Then
            expAssess.Expanded = False
        Else
            expAssess.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelFees_Click(sender As System.Object, e As System.EventArgs) Handles picPanelFees.Click, picFees.Click
        If expFees.Expanded = True Then
            expFees.Expanded = False
        Else
            expFees.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub picPanelSummary_Click(sender As System.Object, e As System.EventArgs) Handles picPanelSummary.Click, picSummary.Click
        If expFees.Expanded = True Then
            expSummary.Expanded = False
        Else
            expSummary.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub closeBuildingSlide(sender As System.Object, e As System.EventArgs) Handles btnBuildingClose.Click
        slideBuilding.IsOpen = False
        panelSideBar.Enabled = True
        picBuilding.Image = My.Resources.Icon_Building2
        expBuilding.Expanded = False
        expBuilding.TitleStyle.ForeColor.Color = Color.White
        expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
    End Sub


    Private Sub expAssessment_ExpandedChanged(sender As Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expAssessment.ExpandedChanged
        If expAssessment.Expanded = True Then
            Call sideBarSelection(expAssessment, picPanelAssessment)
            'False = close other forms; True = Open the Selected Form
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideBuilding.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideAssessment.IsOpen = True
            Call disableRightClick(panelAssessSum) 'Disable right click to avoid pasting on numeric fields
            Call loadBarangaysAssessment() 'load barangay list for search and assessment form : See Region "Fetching File Records to List"
            Call loadSubdivisionsAssessment() 'Load subdivision list for search and assessment form : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expOccupancy_ExpandedChanged(sender As Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expOccupancy.ExpandedChanged
        If expOccupancy.Expanded = True Then
            Call sideBarSelection(expOccupancy, picPanelOccupancy)
            'False = close other forms; True = Open the Selected Form
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideBuilding.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = True
            Call loadBarangaysAssessment()   'load barangay list for search and occupancy form : See Region "Fetching File Records to List"
            Call loadSubdivisionsAssessment() 'Load subdivision list for search and occupancy form : See Region "Fetching File Records to List"
            Call loadClassificationOccupancy() 'Load building classification list for search and occupancy form : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expBuilding_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expBuilding.ExpandedChanged
        If expBuilding.Expanded = True Then
            Call sideBarSelection(expBuilding, picPanelBuilding)
            'False = close other forms; True = Open the Selected Form
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideBuilding.IsOpen = True
            Call loadBuildingClassifications() 'Load Building Classification List : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expBarangay_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expBarangay.ExpandedChanged
        If expBarangay.Expanded = True Then
            Call sideBarSelection(expBarangay, picPanelBarangay)
            'False = close other forms; True = Open the Selected Form
            slideBuilding.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideBarangays.IsOpen = True
            Call loadBarangays() 'Load Building Classification List : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expSubdivision_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expSubdivision.ExpandedChanged
        If expSubdivision.Expanded = True Then
            Call sideBarSelection(expSubdivision, picPanelSubdivision)
            'False = close other forms; True = Open the Selected Form
            slideBuilding.IsOpen = False
            slideBarangays.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideSubdivision.IsOpen = True
            Call loadSubdivisions() 'Load Subdivision List : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If

    End Sub

    Private Sub expAssess_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expAssess.ExpandedChanged
        If expAssess.Expanded = True Then
            Call sideBarSelection(expAssess, picPanelAssess)
            'False = close other forms; True = Open the Selected Form
            slideBuilding.IsOpen = False
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideFees.IsOpen = False
            slideProfile.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideAssessor.IsOpen = True
            Call loadAssessors() 'Load Assessor List : See Region "Fetching File Records to List"
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expFees_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expFees.ExpandedChanged
        If expFees.Expanded = True Then
            Call sideBarSelection(expFees, picPanelFees)
            'False = close other forms; True = Open the Selected Form
            slideBuilding.IsOpen = False
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideProfile.IsOpen = False
            slideAssessment.IsOpen = False
            slideSummary.IsOpen = False
            slideOccupancy.IsOpen = False
            slideFees.IsOpen = True
            Call loadFeeReferences("C:\Databases\bpfau.txt") 'Locates text file being displayed MUST be existing or will throw an error
            panelSideBar.Enabled = False
        End If
    End Sub

    Private Sub expSummary_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expSummary.ExpandedChanged
        If expSummary.Expanded = True Then
            Call sideBarSelection(expSummary, picPanelSummary)
            'False = close other forms; True = Open the Selected Form
            slideBuilding.IsOpen = False
            slideBarangays.IsOpen = False
            slideSubdivision.IsOpen = False
            slideAssessor.IsOpen = False
            slideFees.IsOpen = False
            slideAssessment.IsOpen = False
            slideOccupancy.IsOpen = False
            slideSummary.IsOpen = True
            panelSideBar.Enabled = False
        End If
    End Sub

#End Region

#Region "Fetching File Records to List"

    Private Sub loadBuildingClassifications()
        Dim quer As String = "SELECT ClassID, Division, GenClass, OccupancyChars, Principal from tblclassifications"
        Dim dtClass As DataTable = executeQuery(quer, "tblClasses")
        If dtClass.Rows.Count > 0 Then
            listClassifications.Items.Clear()
            For recCount As Integer = 0 To dtClass.Rows.Count - 1
                Dim listItem As ListViewItem = listClassifications.Items.Add(dtClass.Rows(recCount)(0).ToString)
                listItem.SubItems.Add(dtClass.Rows(recCount)(1).ToString)
                listItem.SubItems.Add(dtClass.Rows(recCount)(2).ToString)
                listItem.SubItems.Add(dtClass.Rows(recCount)(3).ToString)
                listItem.SubItems.Add(dtClass.Rows(recCount)(4).ToString)
            Next
        End If
    End Sub

    Sub loadBarangays()
        Dim quer As String = "SELECT brgyID, brgyName from tblbarangays WHERE DELETED <> 1 ORDER BY brgyID"
        Dim dtBrgy As DataTable = executeQuery(quer, "tblbgy")
        If dtBrgy.Rows.Count > 0 Then
            listbarangay.Items.Clear()
            For recCount As Integer = 0 To dtBrgy.Rows.Count - 1
                Dim listItem As ListViewItem = listbarangay.Items.Add(dtBrgy.Rows(recCount)(0).ToString)
                listItem.SubItems.Add(dtBrgy.Rows(recCount)(1).ToString)
            Next
        Else
            executeQuery("TRUNCATE tblbarangays", "tblbgyTr")
            listbarangay.Items.Clear()
        End If
    End Sub

    Sub loadSubdivisions()
        Dim quer As String = "SELECT subdID, subdName FROM tblsubdivisions WHERE DELETED <> 1 ORDER BY subdID"
        Dim dtSubd As DataTable = executeQuery(quer, "tblSubd")
        If dtSubd.Rows.Count > 0 Then
            listSubdivisions.Items.Clear()
            For recCount As Integer = 0 To dtSubd.Rows.Count - 1
                Dim listItem As ListViewItem = listSubdivisions.Items.Add(dtSubd.Rows(recCount)(0).ToString)
                listItem.SubItems.Add(dtSubd.Rows(recCount)(1).ToString)
            Next
        Else
            executeQuery("TRUNCATE tblsubdivisions", "tblSubdTR")
            listSubdivisions.Items.Clear()
        End If
    End Sub

    Sub loadClassificationOccupancy()
        Dim quer As String = "SELECT ClassID, CONCAT('[',Division,'] ', GenClass) from tblclassifications"
        Dim dtClassi As DataTable = executeQuery(quer, "tblClassesOcc")
        If dtClassi.Rows.Count > 0 Then
            cboOccBuild.Items.Clear()
            cboOccSBuild.Items.Clear()
            ReDim classAssessment(-1)
            For recCount As Integer = 0 To dtClassi.Rows.Count - 1
                ReDim Preserve classAssessment(classAssessment.Length)
                classAssessment(classAssessment.Length - 1) = dtClassi.Rows(recCount)(0).ToString
                cboOccBuild.Items.Add(dtClassi.Rows(recCount)(1).ToString)
                cboOccSBuild.Items.Add(dtClassi.Rows(recCount)(1).ToString)
            Next
        End If
    End Sub

    Sub loadBarangaysAssessment()
        Dim quer As String = "SELECT brgyID, brgyName from tblbarangays ORDER BY brgyID"
        Dim dtBrgy As DataTable = executeQuery(quer, "tblbgy")
        If dtBrgy.Rows.Count > 0 Then
            cbo_Barangay_Assess.Items.Clear()
            cboSearchBRGY.Items.Clear()
            cboOccBrgy.Items.Clear()
            cboPaymentBgy.Items.Clear()
            cboOccSSub.Items.Clear()
            ReDim brgyAssessment(-1)
            For recCount As Integer = 0 To dtBrgy.Rows.Count - 1
                ReDim Preserve brgyAssessment(brgyAssessment.Length)
                brgyAssessment(brgyAssessment.Length - 1) = dtBrgy.Rows(recCount)(0).ToString
                cbo_Barangay_Assess.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboSearchBRGY.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboOccBrgy.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboOccSBrgy.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboPaymentBgy.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
            Next
        End If
    End Sub

    Sub loadSubdivisionsAssessment()
        Dim quer As String = "SELECT subdID, subdName FROM tblsubdivisions ORDER BY subdID"
        Dim dtSubd As DataTable = executeQuery(quer, "tblSubd")
        If dtSubd.Rows.Count > 0 Then
            cboSubdivision_Assess.Items.Clear()
            cboSearchSub.Items.Clear()
            cboOccSubd.Items.Clear()
            cboPaymentSub.Items.Clear()
            cboOccSSub.Items.Clear()
            ReDim subdAssessment(-1)
            For recCount As Integer = 0 To dtSubd.Rows.Count - 1
                ReDim Preserve subdAssessment(subdAssessment.Length)
                subdAssessment(subdAssessment.Length - 1) = dtSubd.Rows(recCount)(0).ToString
                cboSubdivision_Assess.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboSearchSub.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboOccSubd.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboPaymentSub.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboOccSSub.Items.Add(dtSubd.Rows(recCount)(1).ToString)
            Next
        End If
    End Sub


    Sub loadBarangaysAssessment2()
        Dim quer As String = "SELECT brgyID, brgyName from tblbarangays WHERE DELETED <> 1 ORDER BY brgyID"
        Dim dtBrgy As DataTable = executeQuery(quer, "tblbgy")
        If dtBrgy.Rows.Count > 0 Then
            cbo_Barangay_Assess.Items.Clear()
            cboSearchBRGY.Items.Clear()
            cboOccBrgy.Items.Clear()
            cboPaymentBgy.Items.Clear()
            ReDim brgyAssessment(-1)
            For recCount As Integer = 0 To dtBrgy.Rows.Count - 1
                ReDim Preserve brgyAssessment(brgyAssessment.Length)
                brgyAssessment(brgyAssessment.Length - 1) = dtBrgy.Rows(recCount)(0).ToString
                cbo_Barangay_Assess.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboSearchBRGY.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboOccBrgy.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
                cboPaymentBgy.Items.Add(dtBrgy.Rows(recCount)(1).ToString)
            Next
        End If
    End Sub

    Sub loadSubdivisionsAssessment2()
        Dim quer As String = "SELECT subdID, subdName FROM tblsubdivisions WHERE DELETED <> 1 ORDER BY subdID"
        Dim dtSubd As DataTable = executeQuery(quer, "tblSubd")
        If dtSubd.Rows.Count > 0 Then
            cboSubdivision_Assess.Items.Clear()
            cboSearchSub.Items.Clear()
            cboOccSubd.Items.Clear()
            cboPaymentSub.Items.Clear()
            ReDim subdAssessment(-1)
            For recCount As Integer = 0 To dtSubd.Rows.Count - 1
                ReDim Preserve subdAssessment(subdAssessment.Length)
                subdAssessment(subdAssessment.Length - 1) = dtSubd.Rows(recCount)(0).ToString
                cboSubdivision_Assess.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboSearchSub.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboOccSubd.Items.Add(dtSubd.Rows(recCount)(1).ToString)
                cboPaymentSub.Items.Add(dtSubd.Rows(recCount)(1).ToString)
            Next
        End If
    End Sub



    Sub loadAssessors()
        Dim quer As String = "SELECT UserID, UserType, UserDescription, Username, FirstName, LastName, MiddleName, UserPwd, UserImage FROM tblUsers ORDER By CAST(UserID AS Unsigned)"
        Dim dtAssessor As DataTable = executeQuery(quer, "tblAssess")
        If dtAssessor.Rows.Count > 0 Then
            listUsers.Items.Clear()
            For recCount As Integer = 0 To dtAssessor.Rows.Count - 1
                Dim listItem As ListViewItem = listUsers.Items.Add(dtAssessor.Rows(recCount)(3).ToString)
                listItem.SubItems.Add(dtAssessor.Rows(recCount)(5).ToString)
                listItem.SubItems.Add(dtAssessor.Rows(recCount)(4).ToString)
                listItem.SubItems.Add(dtAssessor.Rows(recCount)(6).ToString)
                listItem.SubItems.Add(dtAssessor.Rows(recCount)(2).ToString)
            Next
        End If
    End Sub

    Sub loadFeeReferences(ByVal file As String)
        If System.IO.File.Exists(file) Then
            Dim objReader As New System.IO.StreamReader(file)
            txtFees.Text = objReader.ReadToEnd
        End If
    End Sub

    Sub loadQuestions()
        cboProfileQuestions.Items.Clear()
        Dim q As String = "SELECT Question FROM tblquestions ORDER BY ID ASC"
        Dim dtq As DataTable = executeQuery(q, "getQuestions")
        If dtq.Rows.Count > 0 Then
            For i As Integer = 0 To dtq.Rows.Count - 1
                cboProfileQuestions.Items.Add(dtq.Rows(i)(0).ToString)
            Next
        End If
    End Sub

    Sub loadSystemAccount(ByVal username As String)
        Dim query As String = "SELECT UserID, UserType, UserDescription, Username, FirstName, LastName, MiddleName, UserPwd, UserImage, Question, Answer FROM tblUsers WHERE UserID = '" & initializeQueryEntry(username, True) & "'"
        Dim dtAssessors As DataTable = executeQuery(query, "tblAssessors")
        If dtAssessors.Rows.Count > 0 Then
            If dtAssessors.Rows(0)(8).ToString = "" Or dtAssessors.Rows(0)(8).ToString = String.Empty Then
                picSystemProfile.Image = My.Resources.DefaultUser
            Else
                imageUpload = dtAssessors.Rows(0)(8).ToString
                If IO.File.Exists(dtAssessors.Rows(0)(8).ToString) Then
                    picSystemProfile.Image = Image.FromFile(dtAssessors.Rows(0)(8).ToString)
                Else
                    picSystemProfile.Image = My.Resources.DefaultUser
                End If
            End If

            txtSystemLastname.Text = dtAssessors.Rows(0)(5).ToString
            txtSystemFirstName.Text = dtAssessors.Rows(0)(4).ToString
            txtSystemMiddleName.Text = dtAssessors.Rows(0)(6).ToString
            txtSystemUser.Text = dtAssessors.Rows(0)(3).ToString
            txtUSerDesignation.Text = dtAssessors.Rows(0)(2).ToString
            profilePassword = dtAssessors.Rows(0)(7).ToString
            userNameEdit = dtAssessors.Rows(0)(3).ToString
            cboProfileQuestions.SelectedIndex = CInt(dtAssessors.Rows(0)(9).ToString) - 1
            TextBoxX2.Text = dtAssessors.Rows(0)(10).ToString
        End If
    End Sub

#End Region

#Region "File List Selection Code (Via Clicks and arrow keys)"

    Private Sub listClassifications_Click(sender As System.Object, e As System.EventArgs) Handles listClassifications.Click
        Dim listItem As New ListView
        listItem = listClassifications
        txtClassID.Text = listItem.FocusedItem.SubItems(0).Text.PadLeft(2, "0")
        txtDivision.Text = listItem.FocusedItem.SubItems(1).Text
        txtGenClass.Text = listItem.FocusedItem.SubItems(2).Text
        txtOccupancy.Text = listItem.FocusedItem.SubItems(3).Text
        txtPrincipal.Text = listItem.FocusedItem.SubItems(4).Text
    End Sub

    Private Sub listClassifications_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listClassifications.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Dim listItem As New ListView
            listItem = listClassifications
            txtClassID.Text = listItem.FocusedItem.SubItems(0).Text.PadLeft(2, "0")
            txtDivision.Text = listItem.FocusedItem.SubItems(1).Text
            txtGenClass.Text = listItem.FocusedItem.SubItems(2).Text
            txtOccupancy.Text = listItem.FocusedItem.SubItems(3).Text
            txtPrincipal.Text = listItem.FocusedItem.SubItems(4).Text
        End If
    End Sub

    Private Sub listbarangay_Click(sender As System.Object, e As System.EventArgs) Handles listbarangay.Click
        Dim listItem As New ListView
        listItem = listbarangay
        txtBrgyID.Text = listItem.FocusedItem.SubItems(0).Text
        txtBrgyName.Text = listItem.FocusedItem.SubItems(1).Text
    End Sub

    Private Sub listbarangay_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listbarangay.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Dim listItem As New ListView
            listItem = listbarangay
            txtBrgyID.Text = listItem.FocusedItem.SubItems(0).Text
            txtBrgyName.Text = listItem.FocusedItem.SubItems(1).Text
        End If
    End Sub

    Private Sub listSubdivisions_Click(sender As System.Object, e As System.EventArgs) Handles listSubdivisions.Click
        Dim listItem As New ListView
        listItem = listSubdivisions
        txtSubdID.Text = listItem.FocusedItem.SubItems(0).Text
        txtSubdName.Text = listItem.FocusedItem.SubItems(1).Text
    End Sub

    Private Sub listSubdivisions_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listSubdivisions.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Dim listItem As New ListView
            listItem = listSubdivisions
            txtSubdID.Text = listItem.FocusedItem.SubItems(0).Text
            txtSubdName.Text = listItem.FocusedItem.SubItems(1).Text
        End If
    End Sub

    Private Sub listUsers_Click(sender As System.Object, e As System.EventArgs) Handles listUsers.Click
        Dim listItem As New ListView
        listItem = listUsers
        txtAssessUsername.Text = listItem.FocusedItem.SubItems(0).Text
        txtAssessLastName.Text = listItem.FocusedItem.SubItems(1).Text
        txtAssessFirstName.Text = listItem.FocusedItem.SubItems(2).Text
        txtAssessMiddleName.Text = listItem.FocusedItem.SubItems(3).Text
        txtAssessDesignation.Text = listItem.FocusedItem.SubItems(4).Text
    End Sub

    Private Sub listUsers_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listUsers.KeyUp
        If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
            Dim listItem As New ListView
            listItem = listUsers
            txtAssessUsername.Text = listItem.FocusedItem.SubItems(0).Text
            txtAssessLastName.Text = listItem.FocusedItem.SubItems(1).Text
            txtAssessFirstName.Text = listItem.FocusedItem.SubItems(2).Text
            txtAssessMiddleName.Text = listItem.FocusedItem.SubItems(3).Text
            txtAssessDesignation.Text = listItem.FocusedItem.SubItems(4).Text
        End If
    End Sub

#End Region

#Region "File CRUD"


    Sub saveBarangay(ByVal id As String, ByVal name As String)
        Dim query As String = "INSERT INTO tblbarangays(brgyID,brgyName) VALUES('" & initializeQueryEntry(id, False) & "','" & initializeQueryEntry(name, False) & "')"
        Dim dtbrgy As DataTable = executeQuery(query, "tblbrgy")
    End Sub

    Sub updateBarangay(ByVal id As String, ByVal name As String)
        Dim query As String = "UPDATE tblbarangays SET brgyName  = '" & initializeQueryEntry(name, False) & "' WHERE brgyID = '" & initializeQueryEntry(id, False) & "'"
        Dim dtbrgy As DataTable = executeQuery(query, "tblbrgy")
    End Sub

    Sub deleteBarangay(ByVal id As String)
        Dim query As String = "UPDATE tblbarangays SET deleted = 1 WHERE brgyID= '" & initializeQueryEntry(id, False) & "'"
        Dim dtbrgy As DataTable = executeQuery(query, "tblbrgy")
    End Sub

    Function checkBarangayExist(ByVal name As String, ByVal editID As String) As Boolean
        Dim exist As Boolean = False
        Dim q As String = "SELECT * FROM tblbarangays WHERE brgyName = '" & initializeQueryEntry(name, False) & "' AND brgyID <> '" & initializeQueryEntry(editID, False) & "'"
        Dim dtCheckB As DataTable = executeQuery(q, "checkB")
        If dtCheckB.Rows.Count > 0 Then
            exist = True
        Else
            exist = False
        End If
        Return exist
    End Function

    Function checkSubdivisionExist(ByVal name As String, ByVal editID As String) As Boolean
        Dim exist As Boolean = False
        Dim q As String = "SELECT * FROM tblsubdivisions WHERE subdName = '" & initializeQueryEntry(name, False) & "' AND subdID <> '" & initializeQueryEntry(editID, False) & "'"
        Dim dtCheckS As DataTable = executeQuery(q, "checkS")
        If dtCheckS.Rows.Count > 0 Then
            exist = True
        Else
            exist = False
        End If
        Return exist
    End Function

    Sub saveSubdivision(ByVal id As String, ByVal name As String)
        Dim query As String = "INSERT INTO tblsubdivisions(subdID,subdName) VALUES('" & initializeQueryEntry(id, False) & "','" & initializeQueryEntry(name, False) & "')"
        Dim dtbrgy As DataTable = executeQuery(query, "tblSubd")
    End Sub

    Sub updateSubdivision(ByVal id As String, ByVal name As String)
        Dim query As String = "UPDATE tblsubdivisions SET subdName  = '" & initializeQueryEntry(name, False) & "' WHERE subdID = '" & initializeQueryEntry(id, False) & "'"
        Dim dtbrgy As DataTable = executeQuery(query, "tblSubd")
    End Sub

    Sub deleteSubdivision(ByVal id As String)
        Dim query As String = "UPDATE tblsubdivisions SET deleted = 1 WHERE subdID= '" & initializeQueryEntry(id, False) & "'"
        Dim dtbrgy As DataTable = executeQuery(query, "tblSubd")
    End Sub

    Sub saveAssessmentApplicant(Optional ByVal additional As Boolean = False)
        Dim q As String
        Dim subdivision As String
        If cboSubdivision_Assess.SelectedIndex = -1 Then
            subdivision = ""
        Else
            subdivision = subdAssessment(cboSubdivision_Assess.SelectedIndex)
        End If
        If additional = True Then
            q = "INSERT INTO tblassessmentapplicant(ACN, Date, LastName, FirstName, MiddleName, " & _
           "Project, Lot, Block, Phase, SubdivisionCode,OtherInfo, Zone, BarangayCode, Remarks, PermitPre, PermitSub, PermitDate, encoder, additional) " & _
           "VALUES('" & initializeQueryEntry(txtACN.Text, False) & "','" & initializeQueryEntry(dtAssessDate.Value.ToShortDateString, False) & "','" & initializeQueryEntry(txtAssessLast.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessFirst.Text.Trim, False) & "'," & _
           "'" & initializeQueryEntry(txtAssessMiddle.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessProject.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessLot.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessBlock.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessPhase.Text.Trim, False) & "'," & _
           "'" & initializeQueryEntry(subdivision, False) & "','" & initializeQueryEntry(txtAssessOther.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessZone.Text.Trim, False) & "','" & initializeQueryEntry(brgyAssessment(cbo_Barangay_Assess.SelectedIndex), False) & "'," & _
           "'" & initializeQueryEntry(txtAssessRemarks.Text, False) & "','" & initializeQueryEntry(txtAssessPermitPre.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessPermitSub.Text.Trim, False) & "','" & initializeQueryEntry(dtPermitIssue.Value.ToShortDateString, False) & "','" & initializeQueryEntry(txtAssesCode.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessAdditional.Text.Trim, False) & "')"
        Else
            q = "INSERT INTO tblassessmentapplicant(ACN, Date, LastName, FirstName, MiddleName, " & _
           "Project, Lot, Block, Phase, SubdivisionCode,OtherInfo, Zone, BarangayCode, Remarks, PermitPre, PermitSub, PermitDate, encoder) " & _
           "VALUES('" & initializeQueryEntry(txtACN.Text, False) & "','" & initializeQueryEntry(dtAssessDate.Value.ToShortDateString, False) & "','" & initializeQueryEntry(txtAssessLast.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessFirst.Text.Trim, False) & "'," & _
           "'" & initializeQueryEntry(txtAssessMiddle.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessProject.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessLot.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessBlock.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessPhase.Text.Trim, False) & "'," & _
           "'" & initializeQueryEntry(subdivision, False) & "','" & initializeQueryEntry(txtAssessOther.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessZone.Text.Trim, False) & "','" & initializeQueryEntry(brgyAssessment(cbo_Barangay_Assess.SelectedIndex), False) & "'," & _
           "'" & initializeQueryEntry(txtAssessRemarks.Text, False) & "','" & initializeQueryEntry(txtAssessPermitPre.Text.Trim, False) & "','" & initializeQueryEntry(txtAssessPermitSub.Text.Trim, False) & "','" & initializeQueryEntry(dtPermitIssue.Value.ToShortDateString, False) & "','" & initializeQueryEntry(txtAssesCode.Text.Trim, False) & "')"
        End If

        Dim dtAssessmentSave As DataTable = executeQuery(q, "tblAssessmentSave")
    End Sub

    Sub updateAssessmentApplicant()
        Dim q As String
        Dim subdivision As String
        If cboSubdivision_Assess.SelectedIndex = -1 Then
            subdivision = ""
        Else
            subdivision = subdAssessment(cboSubdivision_Assess.SelectedIndex)
        End If
        q = "UPDATE tblassessmentapplicant SET Date = '" & initializeQueryEntry(dtAssessDate.Value.ToShortDateString, False) & "', LastName = '" & initializeQueryEntry(txtAssessLast.Text.Trim, False) & "'," & _
            "FirstName = '" & initializeQueryEntry(txtAssessFirst.Text.Trim, False) & "', MiddleName = '" & initializeQueryEntry(txtAssessMiddle.Text.Trim, False) & "', " & _
            "Project = '" & initializeQueryEntry(txtAssessProject.Text.Trim, False) & "', Lot = '" & initializeQueryEntry(txtAssessLot.Text.Trim, False) & "'," & _
            "Block = '" & initializeQueryEntry(txtAssessBlock.Text.Trim, False) & "', Phase = '" & initializeQueryEntry(txtAssessPhase.Text.Trim, False) & "'," & _
            "SubdivisionCode = '" & initializeQueryEntry(subdivision, False) & "', OtherInfo = '" & initializeQueryEntry(txtAssessOther.Text.Trim, False) & "'," & _
            "Zone = '" & initializeQueryEntry(txtAssessZone.Text.Trim, False) & "', BarangayCode = '" & initializeQueryEntry(brgyAssessment(cbo_Barangay_Assess.SelectedIndex), False) & "'," & _
            "Remarks = '" & initializeQueryEntry(txtAssessRemarks.Text, False) & "', PermitPre = '" & initializeQueryEntry(txtAssessPermitPre.Text.Trim, False) & "'," & _
            "PermitSub = '" & initializeQueryEntry(txtAssessPermitSub.Text.Trim, False) & "', PermitDate = '" & initializeQueryEntry(dtPermitIssue.Value.ToShortDateString, False) & "'," & _
            "encoder = '" & initializeQueryEntry(txtAssesCode.Text.Trim, False) & "' " & _
            " WHERE ACN = '" & initializeQueryEntry(txtACN.Text, False) & "'"

        Dim dtAssessmentUpdate As DataTable = executeQuery(q, "tblAssessmentUpdate")
    End Sub

    Sub saveAssessmentSummary()
        Dim q As String = "INSERT INTO tblassessmentsummary(ACN, BuildConst, ElecInst, MechIns, PlumbIns, ElectroIns, BuildAcc," & _
                        "OtherAcc, BuildOcc, BuildInsp, CertFee, Fines, TOTALAssess, Local, Natl, OBO) " & _
            "VALUES('" & initializeQueryEntry(txtACN.Text, False) & "','" & CDbl(txtBuildConstFee.Text) & "','" & CDbl(txtElecInsFee.Text) & "','" & CDbl(txtMechInsFee.Text) & "'," & _
            "'" & CDbl(txtPlumbInsFee.Text) & "','" & CDbl(txtElectroInsFee.Text) & "','" & CDbl(txtBuildAccFee.Text) & "','" & CDbl(txtOthrAccFee.Text) & "','" & CDbl(txtBuildOccFee.Text) & "'," & _
            "'" & CDbl(txtBuildInspFee.Text) & "','" & CDbl(txtCertFee.Text) & "','" & CDbl(txtFinesFee.Text) & "','" & CDbl(txtTotalAssessFee.Text) & "'," & _
            "'" & CDbl(txtLocalGov.Text) & "','" & CDbl(txtNatlGov.Text) & "','" & CDbl(txtOBO.Text) & "')"
        Dim dtAssessmentSumSave As DataTable = executeQuery(q, "tblAssessmentSumSave")
    End Sub

    Sub updateAssessmentSummary()
        Dim q As String = "UPDATE tblassessmentsummary SET BuildConst='" & CDbl(txtBuildConstFee.Text) & "',ElecInst='" & CDbl(txtElecInsFee.Text) & "'," & _
                        "MechIns='" & CDbl(txtMechInsFee.Text) & "',PlumbIns='" & CDbl(txtPlumbInsFee.Text) & "',ElectroIns='" & CDbl(txtElectroInsFee.Text) & "'," & _
                        "BuildAcc='" & CDbl(txtBuildAccFee.Text) & "',OtherAcc='" & CDbl(txtOthrAccFee.Text) & "',BuildOcc='" & CDbl(txtBuildOccFee.Text) & "'," & _
                        "BuildInsp='" & CDbl(txtBuildInspFee.Text) & "',CertFee='" & CDbl(txtCertFee.Text) & "',Fines='" & CDbl(txtFinesFee.Text) & "'," & _
                        "TOTALAssess='" & CDbl(txtTotalAssessFee.Text) & "',Local='" & CDbl(txtLocalGov.Text) & "',Natl='" & CDbl(txtNatlGov.Text) & "'," & _
                        "OBO='" & CDbl(txtOBO.Text) & "' WHERE ACN = '" & initializeQueryEntry(txtACN.Text, False) & "'"
        Dim dtAssessmentSumSave As DataTable = executeQuery(q, "tblAssessmentSumUpdate")
    End Sub

    Function checkExistProject(ByVal projectName As String, Optional ByVal acn As String = "") As Boolean
        Dim exist As Boolean = False
        Dim q As String = "SELECT * FROM tblassessmentapplicant WHERE " & _
            "Project = '" & initializeQueryEntry(txtAssessProject.Text.Trim, True) & "' "
        If acn.Trim <> "" Then
            q &= "AND ACN <> '" & initializeQueryEntry(acn, True) & "'"
        End If
        Dim dtACDN As DataTable = executeQuery(q, "checkACN")
        If dtACDN.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Aesthetics"

    'Subroutine being called on selecting item on the sidebar responsible for appearance trasition from purple to white and vice versa
    Private Sub sideBarSelection(ByRef exp As ExpandablePanel, ByRef picPanel As PanelEx)
        picPanel.Style.BackColor1.Color = Color.White
        exp.Expanded = True
        exp.TitleStyle.ForeColor.Color = Color.FromArgb(52, 69, 99)
        exp.Style.BackColor1.Color = Color.White
        Select Case picPanel.Name
            Case picPanelAssessment.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                picSummary.Image = My.Resources.Icon_Calcu
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment_Navy

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelOccupancy.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                picSummary.Image = My.Resources.Icon_Calcu
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy_Navy

                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelBuilding.Name
                picBuilding.Image = My.Resources.Icon_Building_Navy
                picSummary.Image = My.Resources.Icon_Calcu
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelBarangay.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay_Navy
                picSummary.Image = My.Resources.Icon_Calcu
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelSubdivision.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSummary.Image = My.Resources.Icon_Calcu
                picSubd.Image = My.Resources.Icon_Subdivision_Navy
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


            Case picPanelAssess.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picSummary.Image = My.Resources.Icon_Calcu
                picAssess.Image = My.Resources.Icon_Users_Navy
                picFees.Image = My.Resources.Icon_Chart
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelFees.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picSummary.Image = My.Resources.Icon_Calcu
                picFees.Image = My.Resources.Icon_Chart_Navy
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
            Case picPanelSummary.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                picSummary.Image = My.Resources.Icon_Calcu_Navy
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar
                expPayment.Expanded = False
                expPayment.TitleStyle.ForeColor.Color = Color.White
                expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

            Case picPanelPayment.Name
                picBuilding.Image = My.Resources.Icon_Building2
                picBrgy.Image = My.Resources.Icon_Barangay2
                picSubd.Image = My.Resources.Icon_Subdivision3
                picAssess.Image = My.Resources.Icon_Users
                picFees.Image = My.Resources.Icon_Chart
                picSummary.Image = My.Resources.Icon_Calcu
                expBarangay.Expanded = False
                expBarangay.TitleStyle.ForeColor.Color = Color.White
                expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expSubdivision.Expanded = False
                expSubdivision.TitleStyle.ForeColor.Color = Color.White
                expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expAssess.Expanded = False
                expAssess.TitleStyle.ForeColor.Color = Color.White
                expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expBuilding.Expanded = False
                expBuilding.TitleStyle.ForeColor.Color = Color.White
                expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                expFees.Expanded = False
                expFees.TitleStyle.ForeColor.Color = Color.White
                expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picPayment.Image = My.Resources.Icon_Dollar_Navy

                picOccupancy.Image = My.Resources.Icon_Occupancy
                expOccupancy.Expanded = False
                expOccupancy.TitleStyle.ForeColor.Color = Color.White
                expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                expSummary.Expanded = False
                expSummary.TitleStyle.ForeColor.Color = Color.White
                expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

                picAssessment.Image = My.Resources.Icon_Assessment
                expAssessment.Expanded = False
                expAssessment.TitleStyle.ForeColor.Color = Color.White
                expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
                picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        End Select

    End Sub

#End Region

#Region "Key generation : Including ACN and OCN (Occpancy Control Number)"

    Sub generateOCN()
        Dim query As String = "SELECT MAX(OCN) FROM tbloccupancy"
        Dim dtOcc As DataTable = executeQuery(query, "tblOcc")
        Dim IDCounter As Integer = 0
        If dtOcc.Rows.Count > 0 Then
            If dtOcc.Rows(dtOcc.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtOCN.Text = IDCounter.ToString.PadLeft(7, "0")
            Else
                IDCounter = CInt(dtOcc.Rows(dtOcc.Rows.Count - 1)(0).ToString) + 1
                txtOCN.Text = IDCounter.ToString.PadLeft(7, "0")
            End If
        End If
    End Sub

    Sub generateACN()
        Dim query As String = "SELECT MAX(ACN) FROM tblassessmentapplicant"
        Dim dtAssessment As DataTable = executeQuery(query, "tblAssess")
        Dim IDCounter As Integer = 0
        If dtAssessment.Rows.Count > 0 Then
            If dtAssessment.Rows(dtAssessment.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtACN.Text = IDCounter.ToString.PadLeft(7, "0")
            Else
                IDCounter = CInt(dtAssessment.Rows(dtAssessment.Rows.Count - 1)(0).ToString) + 1
                txtACN.Text = IDCounter.ToString.PadLeft(7, "0")
            End If
        End If
    End Sub

    Sub generateBrgyID()
        Dim query As String = "SELECT MAX(brgyID) FROM tblbarangays"
        Dim dtbrgy As DataTable = executeQuery(query, "tblbrgy")
        Dim IDCounter As Integer = 0
        If dtbrgy.Rows.Count > 0 Then
            If dtbrgy.Rows(dtbrgy.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtBrgyID.Text = IDCounter.ToString.PadLeft(2, "0")
            Else
                IDCounter = CInt(dtbrgy.Rows(dtbrgy.Rows.Count - 1)(0).ToString) + 1
                txtBrgyID.Text = IDCounter.ToString.PadLeft(2, "0")
            End If
        End If
    End Sub

    Sub generatesubID()
        Dim query As String = "SELECT MAX(subdID) FROM tblsubdivisions"
        Dim dtsub As DataTable = executeQuery(query, "tblsub")
        Dim IDCounter As Integer = 0
        If dtsub.Rows.Count > 0 Then
            If dtsub.Rows(dtsub.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtSubdID.Text = IDCounter.ToString.PadLeft(2, "0")
            Else
                IDCounter = CInt(dtsub.Rows(dtsub.Rows.Count - 1)(0).ToString) + 1
                txtSubdID.Text = IDCounter.ToString.PadLeft(2, "0")
            End If
        End If
    End Sub

    Sub generatePaymentID()
        Dim query As String = "SELECT MAX(paymentID) FROM tblpayments"
        Dim dtpay As DataTable = executeQuery(query, "tblpay")
        Dim IDCounter As Integer = 0
        If dtpay.Rows.Count > 0 Then
            If dtpay.Rows(dtpay.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtPID.Text = IDCounter.ToString.PadLeft(7, "0")
            Else
                IDCounter = CInt(dtpay.Rows(dtpay.Rows.Count - 1)(0).ToString) + 1
                txtPID.Text = IDCounter.ToString.PadLeft(7, "0")
            End If
        End If
    End Sub

    Sub generateORNumber()
        Dim query As String = "SELECT COUNT(*) FROM tblpayments"
        Dim dtOR As DataTable = executeQuery(query, "tblOR")
        Dim IDCounter As Integer = 0
        If dtOR.Rows.Count > 0 Then
            If dtOR.Rows(dtOR.Rows.Count - 1)(0).ToString = String.Empty Then
                IDCounter += 1
                txtPaymentOR.Text = Format(Date.Today, "MMyy-") & IDCounter.ToString.PadLeft(7, "0")
            Else
                IDCounter = CInt(dtOR.Rows(dtOR.Rows.Count - 1)(0).ToString) + 1
                txtPaymentOR.Text = Format(Date.Today, "MMyy-") & IDCounter.ToString.PadLeft(7, "0")
            End If
        End If
    End Sub
#End Region

#Region "Actions/Commands for Barangay Module"

    Private Sub btnBrgyAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnBrgyAdd.Click
        If btnBrgyAdd.Text = "Add" Then
            listbarangay.Enabled = False
            Call CleanUpFields(panelBarangayEntry)
            txtBrgyID.ReadOnly = True
            Call generateBrgyID()
            txtBrgyName.Focus()
            btnBrgyEdit.Enabled = False
            btnBrgyDelete.Enabled = False
            btnBrgyClose.Text = "Cancel"
            btnBrgyAdd.Text = "Save"
        ElseIf btnBrgyAdd.Text = "Save" Then
            If txtBrgyName.Text.Trim = String.Empty Then
                MessageBoxEx.Show("Barangay name required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If checkBarangayExist(txtBrgyName.Text.Trim, txtBrgyID.Text) Then
                    MessageBoxEx.Show("Barangay name already exist.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                Call saveBarangay(txtBrgyID.Text, txtBrgyName.Text)
                MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnBrgyAdd.Text = "Add"
                Call logtrail(userID, "Add", "Barangay", txtBrgyName.Text.Trim)
                Call lockClearFields(panelBarangayEntry)
                btnBrgyEdit.Enabled = True
                btnBrgyDelete.Enabled = True
                btnBrgyClose.Text = "Close"
                listbarangay.Enabled = True
                Call loadBarangays()
            End If
        End If
    End Sub

    Private Sub btnBrgyClose_Click(sender As System.Object, e As System.EventArgs) Handles btnBrgyClose.Click
        If btnBrgyClose.Text = "Close" Then
            slideBarangays.IsOpen = False
            Call lockClearFields(panelBarangayEntry)
            panelSideBar.Enabled = True
            picBrgy.Image = My.Resources.Icon_Barangay2
            expBarangay.Expanded = False
            expBarangay.TitleStyle.ForeColor.Color = Color.White
            expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
            picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        Else
            btnBrgyAdd.Text = "Add"
            btnBrgyEdit.Text = "Edit"
            Call lockClearFields(panelBarangayEntry)
            btnBrgyEdit.Enabled = True
            btnBrgyAdd.Enabled = True
            btnBrgyDelete.Enabled = True
            btnBrgyClose.Text = "Close"
            listbarangay.Enabled = True
        End If

    End Sub

    Private Sub btnBrgyEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnBrgyEdit.Click
        If txtBrgyID.Text <> String.Empty Then
            If btnBrgyEdit.Text = "Edit" Then
                listbarangay.Enabled = False
                Call CleanUpFields(panelBarangayEntry, "Edit")
                txtBrgyID.ReadOnly = True
                txtBrgyName.Focus()
                btnBrgyAdd.Enabled = False
                btnBrgyDelete.Enabled = False
                btnBrgyClose.Text = "Cancel"
                btnBrgyEdit.Text = "Update"
            ElseIf btnBrgyEdit.Text = "Update" Then
                If txtBrgyName.Text.Trim = String.Empty Then
                    MessageBoxEx.Show("Barangay name required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If checkBarangayExist(txtBrgyName.Text.Trim, txtBrgyID.Text) Then
                        MessageBoxEx.Show("Barangay name already exist.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    Call updateBarangay(txtBrgyID.Text, txtBrgyName.Text)
                    MessageBoxEx.Show("Record successfully updated.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnBrgyEdit.Text = "Edit"
                    Call logtrail(userID, "Edit", "Barangay", txtBrgyName.Text.Trim)
                    Call lockClearFields(panelBarangayEntry)
                    btnBrgyAdd.Enabled = True
                    btnBrgyDelete.Enabled = True
                    btnBrgyClose.Text = "Close"
                    Call loadBarangays()
                    listbarangay.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub btnBrgyDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnBrgyDelete.Click
        If txtBrgyID.Text.Trim = String.Empty Then
            MessageBoxEx.Show("Select a record to delete.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If MessageBoxEx.Show("Delete selected record?", My.Resources.PopupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Call deleteBarangay(txtBrgyID.Text)
                MessageBoxEx.Show("Record successfully deleted.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call loadBarangays()
                Call logtrail(userID, "Delete", "Barangay", txtBrgyName.Text.Trim)
                Call lockClearFields(panelBarangayEntry)
            End If
        End If
    End Sub

#End Region

#Region "Actions/Commands for Subdivision Module"

    Private Sub btnsubAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnSubAdd.Click
        If btnSubAdd.Text = "Add" Then
            listSubdivisions.Enabled = False
            Call CleanUpFields(panelSubdivisionEntry)
            txtSubdID.ReadOnly = True
            Call generatesubID()
            txtSubdName.Focus()
            btnSubEdit.Enabled = False
            btnSubDelete.Enabled = False
            btnSubClose.Text = "Cancel"
            btnSubAdd.Text = "Save"
        ElseIf btnSubAdd.Text = "Save" Then
            If txtSubdName.Text.Trim = String.Empty Then
                MessageBoxEx.Show("Subdivision name required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                If checkSubdivisionExist(txtSubdName.Text.Trim, txtSubdID.Text) Then
                    MessageBoxEx.Show("Subdivision name already exist.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                Call saveSubdivision(txtSubdID.Text, txtSubdName.Text)
                MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnSubAdd.Text = "Add"
                Call logtrail(userID, "Add", "Subdivision", txtSubdName.Text.Trim)
                Call lockClearFields(panelSubdivisionEntry)
                listSubdivisions.Enabled = True
                btnSubEdit.Enabled = True
                btnSubDelete.Enabled = True
                btnSubClose.Text = "Close"
                Call loadSubdivisions()

            End If
        End If
    End Sub

    Private Sub btnsubClose_Click(sender As System.Object, e As System.EventArgs) Handles btnSubClose.Click
        If btnSubClose.Text = "Close" Then
            slideSubdivision.IsOpen = False
            Call lockClearFields(panelSubdivisionEntry)
            panelSideBar.Enabled = True
            picSubd.Image = My.Resources.Icon_Subdivision3
            expSubdivision.Expanded = False
            expSubdivision.TitleStyle.ForeColor.Color = Color.White
            expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
            picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        Else
            btnSubAdd.Text = "Add"
            btnSubEdit.Text = "Edit"
            Call lockClearFields(panelSubdivisionEntry)
            btnSubEdit.Enabled = True
            btnSubAdd.Enabled = True
            btnSubDelete.Enabled = True
            btnSubClose.Text = "Close"
            listSubdivisions.Enabled = True
        End If

    End Sub

    Private Sub btnsubEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnSubEdit.Click
        If txtSubdID.Text <> String.Empty Then
            If btnSubEdit.Text = "Edit" Then
                listSubdivisions.Enabled = False
                Call CleanUpFields(panelSubdivisionEntry, "Edit")
                txtSubdID.ReadOnly = True
                txtSubdName.Focus()
                btnSubAdd.Enabled = False
                btnSubDelete.Enabled = False
                btnSubClose.Text = "Cancel"
                btnSubEdit.Text = "Update"
            ElseIf btnSubEdit.Text = "Update" Then
                If txtSubdName.Text.Trim = String.Empty Then
                    MessageBoxEx.Show("Subdivision name required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    If checkSubdivisionExist(txtSubdName.Text.Trim, txtSubdID.Text) Then
                        MessageBoxEx.Show("Subdivision name already exist.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    Call updateSubdivision(txtSubdID.Text, txtSubdName.Text)
                    MessageBoxEx.Show("Record successfully updated.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btnSubEdit.Text = "Edit"
                    Call logtrail(userID, "Edit", "Subdivision", txtSubdName.Text.Trim)
                    Call lockClearFields(panelSubdivisionEntry)
                    listSubdivisions.Enabled = True
                    btnSubAdd.Enabled = True
                    btnSubDelete.Enabled = True
                    btnSubClose.Text = "Close"
                    Call loadSubdivisions()
                End If
            End If
        End If
    End Sub

    Private Sub btnsubDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnSubDelete.Click
        If txtSubdID.Text.Trim = String.Empty Then
            MessageBoxEx.Show("Select a record to delete.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            If MessageBoxEx.Show("Delete selected record?", My.Resources.PopupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                Call deleteSubdivision(txtSubdID.Text)
                MessageBoxEx.Show("Record successfully deleted.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call loadSubdivisions()
                Call logtrail(userID, "Delete", "Subdivision", txtSubdName.Text.Trim)
                Call lockClearFields(panelSubdivisionEntry)
            End If
        End If
    End Sub

#End Region

#Region "Actions/Commands for Profile Popup and Modules"


    Private Sub ButtonX2_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX2.Click
        If MessageBoxEx.Show("Close BPFAU?", My.Resources.PopupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Call logtrail(userID, "Sign Out", "Accounts", "***")
            End
        End If
    End Sub

    Private Sub ButtonX3_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX3.Click
        Call loadQuestions()
        Call loadSystemAccount(userID)
        Call logtrail(userID, "View Profile", "Accounts", lblLoggedName.Text)
        panelAccount.Visible = False
        slideBuilding.IsOpen = False
        slideBarangays.IsOpen = False
        slideSubdivision.IsOpen = False
        slideAssessor.IsOpen = False
        slideFees.IsOpen = False
        slideAssessment.IsOpen = False
        slideOccupancy.IsOpen = False
        slideSummary.IsOpen = False
        slidePayment.IsOpen = False

        picBuilding.Image = My.Resources.Icon_Building2
        expBuilding.Expanded = False
        expBuilding.TitleStyle.ForeColor.Color = Color.White
        expBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelBuilding.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


        picBrgy.Image = My.Resources.Icon_Barangay2
        expBarangay.Expanded = False
        expBarangay.TitleStyle.ForeColor.Color = Color.White
        expBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelBarangay.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


        picSubd.Image = My.Resources.Icon_Subdivision3
        expSubdivision.Expanded = False
        expSubdivision.TitleStyle.ForeColor.Color = Color.White
        expSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelSubdivision.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


        picAssess.Image = My.Resources.Icon_Users
        expAssess.Expanded = False
        expAssess.TitleStyle.ForeColor.Color = Color.White
        expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

        picFees.Image = My.Resources.Icon_Chart
        expFees.Expanded = False
        expFees.TitleStyle.ForeColor.Color = Color.White
        expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


        picSummary.Image = My.Resources.Icon_Calcu
        expSummary.Expanded = False
        expSummary.TitleStyle.ForeColor.Color = Color.White
        expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

        picOccupancy.Image = My.Resources.Icon_Occupancy
        expOccupancy.Expanded = False
        expOccupancy.TitleStyle.ForeColor.Color = Color.White
        expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)


        picAssessment.Image = My.Resources.Icon_Assessment
        expAssessment.Expanded = False
        expAssessment.TitleStyle.ForeColor.Color = Color.White
        expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

        picPayment.Image = My.Resources.Icon_Dollar
        expPayment.Expanded = False
        expPayment.TitleStyle.ForeColor.Color = Color.White
        expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)

        panelSideBar.Enabled = True

        slideProfile.IsOpen = True
    End Sub

    Private Sub linkChangePassword_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles linkChangePassword.LinkClicked
        panelPassword.Visible = True
        slideProfile.Enabled = False
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If ofdImageUpload.ShowDialog = Windows.Forms.DialogResult.OK Then
            imageUpload = ofdImageUpload.FileName
            picSystemProfile.Image = Image.FromFile(imageUpload)
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If txtSystemFirstName.Text.Trim = "" Then
            MessageBoxEx.Show("First name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSystemFirstName.Focus()
            Exit Sub
        End If
        If txtSystemLastname.Text.Trim = "" Then
            MessageBoxEx.Show("Last name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSystemLastname.Focus()
            Exit Sub
        End If
        If cboProfileQuestions.SelectedIndex = -1 Then
            MessageBoxEx.Show("Security question is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboProfileQuestions.Focus()
            Exit Sub
        End If
        If TextBoxX2.Text.Trim = "" Then
            MessageBoxEx.Show("Security answer is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxX2.Focus()
            Exit Sub
        End If
        Dim q As String = "UPDATE tblUsers SET FirstName = '" & initializeQueryEntry(txtSystemFirstName.Text.Trim, False) & "'," & _
                    "LastName = '" & initializeQueryEntry(txtSystemLastname.Text.Trim, False) & "'," & _
                    "MiddleName = '" & initializeQueryEntry(txtSystemMiddleName.Text.Trim, False) & "'," & _
                    "UserImage = '" & initializeQueryEntry(imageUpload, False) & "', " & _
                    "Question = '" & cboProfileQuestions.SelectedIndex + 1 & "', " & _
                    "Answer = '" & initializeQueryEntry(TextBoxX2.Text, False) & "' " & _
                    "WHERE UserID = '" & initializeQueryEntry(userID, True) & "'"
        Dim dtSave As DataTable = executeQuery(q, "tblSaveProfile")
        MessageBoxEx.Show("Profile successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        lblLoggedName.Text = txtSystemFirstName.Text.Trim & " " & txtSystemLastname.Text.Trim
        If imageUpload = "" Then
            picLoggedImage.Image = My.Resources.DefaultUser
            picAccount.Image = My.Resources.DefaultUser
        Else
            picLoggedImage.Image = Image.FromFile(imageUpload)
            picAccount.Image = Image.FromFile(imageUpload)
        End If
        slideProfile.IsOpen = False
    End Sub

    Private Sub btnpassClose_Click(sender As System.Object, e As System.EventArgs) Handles btnpassClose.Click
        panelPassword.Visible = False
        slideProfile.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        slideProfile.IsOpen = False
        panelSideBar.Enabled = True
    End Sub

    Private Sub btnpassave_Click(sender As System.Object, e As System.EventArgs) Handles btnpassave.Click
        If txtChangePass.Text.Trim = "" Then
            MessageBoxEx.Show("Please enter your current password.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtChangePass.Focus()
            Exit Sub
        End If
        If txtCPNew.Text.Trim = "" Or (txtCPNew.Text.Trim = "" And txtCPReNew.Text.Trim = "") Then
            MessageBoxEx.Show("New Password is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCPNew.Focus()
            Exit Sub
        End If
        If (txtCPNew.Text.Trim <> "" And txtCPNew.Text.Trim <> txtCPReNew.Text.Trim) Or _
            (txtCPReNew.Text.Trim <> "" And txtCPReNew.Text.Trim <> txtCPNew.Text.Trim) Then
            MessageBoxEx.Show("Passwords not matched.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCPNew.Focus()
            Exit Sub
        End If
        Dim query As String = "SELECT UserID, UserType, UserDescription, Username, CONCAT(FirstName,' ',LastName), MiddleName, UserPwd, UserImage FROM tblUsers WHERE UserID = '" & userID & "' " & _
                        "AND UserName = '" & initializeQueryEntry(userNameEdit, True) & "' " & _
                        "AND UserPwd = MD5('" & initializeQueryEntry(txtChangePass.Text.Trim, True) & "')"
        Dim dtLogin As DataTable = executeQuery(query, "tblUsers")
        If dtLogin.Rows.Count > 0 Then
            Dim q As String = "UPDATE tblUsers SET UserPwd = MD5('" & initializeQueryEntry(txtCPNew.Text.Trim, False) & "') WHERE UserID = '" & initializeQueryEntry(userID, True) & "'"
            Dim dtSavePass As DataTable = executeQuery(q, "tblSavePass")
            MessageBoxEx.Show("Password successfully changed.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call logtrail(userID, "Change Password", "Accounts", "***")
            panelPassword.Visible = False
            slideProfile.Enabled = True
        Else
            MessageBoxEx.Show("Invalid password.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If


    End Sub

    Private Sub ButtonX1_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX1.Click
        If MessageBoxEx.Show("This will end your current session." & vbNewLine & "Do you want to continue?", My.Resources.PopupTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            frmLogin1 = New formLogin
            frmLogin1.Show()
            frmLogin1.slideNewUser.IsOpen = True
            Me.Close()
            sqlCon.Close()
        End If
    End Sub


#End Region

    Private Sub btnCloseUsers_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseUsers.Click
        slideAssessor.IsOpen = False
        txtAssessFirstName.Text = ""
        txtAssessLastName.Text = ""
        txtAssessMiddleName.Text = ""
        txtAssessUsername.Text = ""
        txtAssessDesignation.Text = ""
        panelSideBar.Enabled = True
        picAssess.Image = My.Resources.Icon_Users
        expAssess.Expanded = False
        expAssess.TitleStyle.ForeColor.Color = Color.White
        expAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelAssess.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
    End Sub

#Region "Number Input Validation for Assessment Form"

    Private Sub txtBuildConstFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuildConstFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtBuildConstFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtElecInsFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtElecInsFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtElecInsFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtMechInsFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMechInsFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtMechInsFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtPlumbInsFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlumbInsFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtPlumbInsFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtElectroInsFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtElectroInsFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtElectroInsFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtBuildAccFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuildAccFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtBuildAccFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtOthrAccFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOthrAccFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOthrAccFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtBuildOccFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuildOccFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtBuildOccFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtBuildInspFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuildInspFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtBuildInspFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtCertFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCertFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtCertFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtFinesFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFinesFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtFinesFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtTotalAssessFee_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalAssessFee.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtTotalAssessFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtLocalGov_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtLocalGov.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtLocalGov.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtNatlGov_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNatlGov.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtNatlGov.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtOBO_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOBO.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOBO.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    'Function below converts respective field's value into currency format upon losing focus (example is 1000 to 1,000.00 when mouse left)

    Private Sub txtBuildConstFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtBuildConstFee.Leave
        If txtBuildConstFee.Text.Trim = "" Then
            txtBuildConstFee.Text = "0.00"
        Else
            txtBuildConstFee.Text = FormatNumber(txtBuildConstFee.Text, 2)
        End If
    End Sub

    Private Sub txtElecInsFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtElecInsFee.Leave
        'txtElecInsFee.Text = FormatNumber(txtElecInsFee.Text, 2)
        If txtElecInsFee.Text.Trim = "" Then
            txtElecInsFee.Text = "0.00"
        Else
            txtElecInsFee.Text = FormatNumber(txtElecInsFee.Text, 2)
        End If
    End Sub

    Private Sub txtMechInsFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtMechInsFee.Leave
        'txtMechInsFee.Text = FormatNumber(txtMechInsFee.Text, 2)
        If txtMechInsFee.Text.Trim = "" Then
            txtMechInsFee.Text = "0.00"
        Else
            txtMechInsFee.Text = FormatNumber(txtMechInsFee.Text, 2)
        End If
    End Sub

    Private Sub txtPlumbInsFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtPlumbInsFee.Leave
        'txtPlumbInsFee.Text = FormatNumber(txtPlumbInsFee.Text, 2)
        If txtPlumbInsFee.Text.Trim = "" Then
            txtPlumbInsFee.Text = "0.00"
        Else
            txtPlumbInsFee.Text = FormatNumber(txtPlumbInsFee.Text, 2)
        End If
    End Sub

    Private Sub txtElectroInsFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtElectroInsFee.Leave
        'txtElectroInsFee.Text = FormatNumber(txtElectroInsFee.Text, 2)
        If txtElectroInsFee.Text.Trim = "" Then
            txtElectroInsFee.Text = "0.00"
        Else
            txtElectroInsFee.Text = FormatNumber(txtElectroInsFee.Text, 2)
        End If
    End Sub

    Private Sub txtBuildAccFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtBuildAccFee.Leave
        'txtBuildAccFee.Text = FormatNumber(txtBuildAccFee.Text, 2)
        If txtBuildAccFee.Text.Trim = "" Then
            txtBuildAccFee.Text = "0.00"
        Else
            txtBuildAccFee.Text = FormatNumber(txtBuildAccFee.Text, 2)
        End If
    End Sub

    Private Sub txtOthrAccFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtOthrAccFee.Leave
        'txtOthrAccFee.Text = FormatNumber(txtOthrAccFee.Text, 2)
        If txtOthrAccFee.Text.Trim = "" Then
            txtOthrAccFee.Text = "0.00"
        Else
            txtOthrAccFee.Text = FormatNumber(txtOthrAccFee.Text, 2)
        End If
    End Sub

    Private Sub txtBuildOccFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtBuildOccFee.Leave
        'txtBuildOccFee.Text = FormatNumber(txtBuildOccFee.Text, 2)
        If txtBuildOccFee.Text.Trim = "" Then
            txtBuildOccFee.Text = "0.00"
        Else
            txtBuildOccFee.Text = FormatNumber(txtBuildOccFee.Text, 2)
        End If
    End Sub

    Private Sub txtBuildInspFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtBuildInspFee.Leave
        'txtBuildInspFee.Text = FormatNumber(txtBuildInspFee.Text, 2)
        If txtBuildInspFee.Text.Trim = "" Then
            txtBuildInspFee.Text = "0.00"
        Else
            txtBuildInspFee.Text = FormatNumber(txtBuildInspFee.Text, 2)
        End If
    End Sub

    Private Sub txtCertFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtCertFee.Leave
        'txtCertFee.Text = FormatNumber(txtCertFee.Text, 2)
        If txtCertFee.Text.Trim = "" Then
            txtCertFee.Text = "0.00"
        Else
            txtCertFee.Text = FormatNumber(txtCertFee.Text, 2)
        End If
    End Sub

    Private Sub txtFinesFee_Leave(sender As System.Object, e As System.EventArgs) Handles txtFinesFee.Leave
        'txtFinesFee.Text = FormatNumber(txtFinesFee.Text, 2)
        If txtFinesFee.Text.Trim = "" Then
            txtFinesFee.Text = "0.00"
        Else
            txtFinesFee.Text = FormatNumber(txtFinesFee.Text, 2)
        End If
    End Sub

    'Disbling right click from numeric fields to avoid alphabet input
    Sub disableRightClick(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                Dim _blankContextMenu As New ContextMenu()
                txt.ContextMenu = _blankContextMenu
            End If
        Next
    End Sub

#End Region

#Region "Field Reset and Clean Up after saving record or cancel input"

    Sub readOnly2Mode(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                If txt.Tag = "readonly2" Then
                    txt.ReadOnly = True
                End If
            End If
            If TypeOf ctrl Is ComboBox Then
                Dim cbo As New ComboBox
                cbo = ctrl
                If cbo.Tag = "readonly2" Then
                    cbo.Enabled = False
                End If
            End If
        Next
    End Sub

    'Deleting/clearing input contents
    Sub clearAllText(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                txt.Text = ""
            End If
            If TypeOf ctrl Is ComboBox Then
                Dim cbo As New ComboBox
                cbo = ctrl
                cbo.SelectedIndex = -1
            End If
        Next
    End Sub

    'Makes fields writable
    Sub writeAllText(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                If txt.Tag <> "readonly" Then
                    txt.ReadOnly = False
                End If
            End If
            If TypeOf ctrl Is ComboBox Then
                Dim cbo As New ComboBox
                cbo = ctrl
                cbo.Enabled = True
            End If
            If TypeOf ctrl Is DateTimePicker Then
                Dim dt As New DateTimePicker
                dt = ctrl
                dt.Enabled = True
            End If
        Next
    End Sub

    'Make Inputs Read-Only
    Sub readAllText(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                txt.ReadOnly = True
            End If
            If TypeOf ctrl Is ComboBox Then
                Dim cbo As New ComboBox
                cbo = ctrl
                cbo.Enabled = False
            End If
            If TypeOf ctrl Is DateTimePicker Then
                Dim dt As New DateTimePicker
                dt = ctrl
                dt.Enabled = False
            End If
        Next
    End Sub

    'Reseting numeric input to value "0" (zero)
    Sub resetZeroAllText(ByRef parent As PanelEx)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is DevComponents.DotNetBar.Controls.TextBoxX Then
                Dim txt As New DevComponents.DotNetBar.Controls.TextBoxX
                txt = ctrl
                txt.Text = "0.00"
            End If
        Next
    End Sub

#End Region



    Private Sub btnAssessmentClose_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentClose.Click
        If btnAssessmentClose.Text = "&Cancel" Then
            clearAllText(panelAssessSum)
            clearAllText(panelAssessment)
            resetZeroAllText(panelAssessSum)
            readAllText(panelAssessment)
            btnAssessmentFind.Enabled = True
            btnAssessmentMerge.Enabled = False
            btnAssessmentAdditional.Enabled = False
            btnAssessmentClose.Text = "&Close"
            btnAssessmentNew.Text = "&New"
            lblAdd.Visible = False
            txtAssessAdditional.Visible = False
            Call loadBarangaysAssessment()
            Call loadSubdivisionsAssessment()
            btnAssessmentEdit.Enabled = False
            btnAssessmentNew.Enabled = True
            btnAssessmentEdit.Text = "&Edit"
        Else
            slideAssessment.IsOpen = False
            panelSideBar.Enabled = True
            picAssessment.Image = My.Resources.Icon_Assessment
            expAssessment.Expanded = False
            expAssessment.TitleStyle.ForeColor.Color = Color.White
            expAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
            picPanelAssessment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        End If
    End Sub

    Sub generatePermitNo()
        Dim pre As String, subP As String = "0000001"
        Dim q As String = "SELECT MAX(CAST(PermitSub AS UNSIGNED)) + 1 FROM tblassessmentapplicant WHERE Additional = 'NA'"
        Dim dt As DataTable = executeQuery(q, "tablePermit")
        If dt.Rows.Count > 0 Then
            subP = dt.Rows(0)(0).ToString.PadLeft(7, "0")
        End If
        If subP = "0000000" Then
            subP = "0000001"
        End If
        pre = DateTime.Now.ToString("yy")
        txtAssessPermitPre.Text = pre
        txtAssessPermitSub.Text = subP
    End Sub


    Private Sub btnAssessmentNew_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentNew.Click
        If btnAssessmentNew.Text = "&New" Then
            btnAssessmentNew.Text = "&Save"
            btnAssessmentClose.Text = "&Cancel"
            btnAssessmentEdit.Enabled = False
            Call clearAllText(panelAssessment)
            Call resetZeroAllText(panelAssessSum)
            Call writeAllText(panelAssessment)
            Call writeAllText(panelAssessSum)
            Call generateACN()
            Call loadBarangaysAssessment2()
            Call loadSubdivisionsAssessment2()
            Call generatePermitNo()
            dtAssessDate.Value = Today
            txtAssesCode.Text = userID.ToString.PadLeft(3, "0")
            txtAssessUser.Text = loggedUserName
            btnAssessmentFind.Enabled = False
            btnAssessmentMerge.Enabled = False
            txtAssessLast.Focus()
            lblAdd.Visible = False
            txtAssessAdditional.Visible = False
            txtAssessStatus.Text = "Pending"
            btnAssessmentEdit.Enabled = False
        Else
            If txtAssessLast.Text = "" Then
                MessageBoxEx.Show("Family name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessLast.Focus()
                Exit Sub
            End If
            If txtAssessFirst.Text = "" Then
                MessageBoxEx.Show("First name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessFirst.Focus()
                Exit Sub
            End If
            If txtAssessProject.Text = "" Then
                MessageBoxEx.Show("Project is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessProject.Focus()
                Exit Sub
            End If
            If cbo_Barangay_Assess.SelectedIndex = -1 Then
                MessageBoxEx.Show("Barangay is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cbo_Barangay_Assess.Focus()
                Exit Sub
            End If
            If checkExistProject(txtAssessProject.Text.Trim) Then
                MessageBoxEx.Show("Project name already exists.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessProject.Focus()
                Exit Sub
            End If
            If CDbl(txtTotalAssessFee.Text.Trim) < 0 Then
                MessageBoxEx.Show("Total assessment amount is less than 0.00. Please input valid fees.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If CDbl(txtTotalAssessFee.Text.Trim) = 0 Then
                MessageBoxEx.Show("Total assessment amount is 0.00. Please input valid fees.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Call saveAssessmentApplicant(additionalAssessment)
            Call saveAssessmentSummary()
            MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call logtrail(userID, IIf(additionalAssessment, "Additional", "Add"), "Assessment", txtACN.Text.Trim)
            Call readAllText(panelAssessment)
            Call readAllText(panelAssessSum)
            Call resetZeroAllText(panelAssessSum)
            Call clearAllText(panelAssessment)
            Call loadBarangaysAssessment()
            Call loadSubdivisionsAssessment()
            btnAssessmentFind.Enabled = True
            btnAssessmentMerge.Enabled = False
            btnAssessmentAdditional.Enabled = False
            btnAssessmentNew.Text = "&New"
            btnAssessmentClose.Text = "&Close"
            additionalAssessment = False
        End If
    End Sub

    Private Sub txtBuildConstFee_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBuildConstFee.KeyUp, txtElecInsFee.KeyUp, txtMechInsFee.KeyUp, txtPlumbInsFee.KeyUp, txtElectroInsFee.KeyUp, txtBuildAccFee.KeyUp, txtOthrAccFee.KeyUp, txtBuildOccFee.KeyUp, txtBuildInspFee.KeyUp, txtCertFee.KeyUp, txtFinesFee.KeyUp
        Dim totalAssessmentFee As Double, localShare As Double, nationalShare As Double, OBOFee As Double
        totalAssessmentFee = CDbl(IIf(txtBuildConstFee.Text = "", 0, txtBuildConstFee.Text)) + CDbl(IIf(txtElecInsFee.Text = "", 0, txtElecInsFee.Text)) +
            CDbl(IIf(txtMechInsFee.Text = "", 0, txtMechInsFee.Text)) + CDbl(IIf(txtPlumbInsFee.Text = "", 0, txtPlumbInsFee.Text)) +
            CDbl(IIf(txtElectroInsFee.Text = "", 0, txtElectroInsFee.Text)) + CDbl(IIf(txtBuildAccFee.Text = "", 0, txtBuildAccFee.Text)) +
            CDbl(IIf(txtOthrAccFee.Text = "", 0, txtOthrAccFee.Text)) + CDbl(IIf(txtBuildOccFee.Text = "", 0, txtBuildOccFee.Text)) +
            CDbl(IIf(txtBuildInspFee.Text = "", 0, txtBuildInspFee.Text)) + CDbl(IIf(txtCertFee.Text = "", 0, txtCertFee.Text)) +
            CDbl(IIf(txtFinesFee.Text = "", 0, txtFinesFee.Text))
        localShare = totalAssessmentFee * 0.8
        nationalShare = totalAssessmentFee * 0.05
        OBOFee = totalAssessmentFee * 0.15
        txtTotalAssessFee.Text = FormatNumber(totalAssessmentFee, 2)
        txtLocalGov.Text = FormatNumber(localShare, 2)
        txtNatlGov.Text = FormatNumber(nationalShare, 2)
        txtOBO.Text = FormatNumber(OBOFee, 2)
    End Sub

    Private Sub panelAccount_Leave(sender As System.Object, e As System.EventArgs) Handles panelAccount.Leave
        panelAccount.Visible = False
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Call searchApplicant()
    End Sub

    Sub searchApplicant()
        Dim subdivision As String
        If cboSearchSub.SelectedIndex = -1 Then
            subdivision = ""
        Else
            subdivision = subdAssessment(cboSearchSub.SelectedIndex)
        End If
        Dim brgay As String
        If cboSearchBRGY.SelectedIndex = -1 Then
            brgay = ""
        Else
            brgay = subdAssessment(cboSearchBRGY.SelectedIndex)
        End If
        Dim q As String = "SELECT tblAssessApp.ACN, Date, LastName, FirstName, MiddleName, Project, Lot, Block, Phase, SubdName, " & _
                    "OtherInfo, Zone, BrgyName, Remarks, CONCAT(PermitPre,'-', PermitSub), PermitDate, TOTALAssess,Additional, Encoder, PermitPre, PermitSub " & _
                    "FROM tblassessmentapplicant tblAssessApp " & _
                    "LEFT JOIN tblassessmentsummary tblAssessSum ON tblAssessApp.ACN = tblAssessSum.ACN " & _
                    "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
                    "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID " & _
                    "WHERE (LastName LIKE '" & initializeQueryEntry(txtSearchL.Text.Trim, False) & "%' " & _
                    "AND FirstName LIKE '" & initializeQueryEntry(txtSearchF.Text.Trim, False) & "%' " & _
                    "AND MiddleName LIKE '" & initializeQueryEntry(txtSearchM.Text.Trim, False) & "%' " & _
                    "AND Project LIKE '" & initializeQueryEntry(txtSearchPro.Text.Trim, False) & "%' " & _
                    "AND Lot LIKE '" & initializeQueryEntry(txtSearchLot.Text.Trim, False) & "%' " & _
                    "AND Block LIKE '" & initializeQueryEntry(txtSearchBlk.Text.Trim, False) & "%' " & _
                    "AND Phase LIKE '" & initializeQueryEntry(txtSearchPH.Text.Trim, False) & "%' " & _
                    "AND SubDivisionCode LIKE '" & initializeQueryEntry(subdivision, False) & "%' " & _
                    "AND OtherInfo LIKE '" & initializeQueryEntry(txtSearchOther.Text.Trim, False) & "%' " & _
                    "AND Zone LIKE '" & initializeQueryEntry(txtSearchZone.Text.Trim, False) & "%' " & _
                    "AND BarangayCode LIKE '" & initializeQueryEntry(brgay, False) & "%') "

        Dim optionalQue As String = ""
        If searchmode = "Payment" Then
            optionalQue = " AND tblAssessApp.Status = 'Pending' "
        End If
        Dim dtSearch As DataTable = executeQuery(q & optionalQue, "tblSearch")
        If dtSearch.Rows.Count > 0 Then
            btnSearchClose.Text = "Clear"
            listSearchList.Items.Clear()
            For reccount As Integer = 0 To dtSearch.Rows.Count - 1
                Dim listItem As ListViewItem = listSearchList.Items.Add(dtSearch.Rows(reccount)(0).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(2).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(3).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(4).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(5).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(6).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(7).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(8).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(9).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(10).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(11).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(12).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(1).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(14).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(15).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(18).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(17).ToString)
                listItem.SubItems.Add(FormatNumber(dtSearch.Rows(reccount)(16).ToString, 2))
            Next
        Else
            btnSearchClose.Text = "Close"
            listSearchList.Items.Clear()
        End If
    End Sub

    Private Sub btnAssessmentAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentFind.Click
        Call loadBarangaysAssessment()
        Call loadSubdivisionsAssessment()
        slideAssessment.Enabled = False
        panelSearch.Visible = True
        searchmode = "Assessment"
    End Sub

    Private Sub btnSearchClose_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchClose.Click
        If btnSearchClose.Text = "Clear" Then
            Call clearAllText(panelInnerSearch)
            listSearchList.Items.Clear()
            cboSearchBRGY.SelectedIndex = -1
            cboSearchSub.SelectedIndex = -1
            btnSearchClose.Text = "Close"
        Else
            slideAssessment.Enabled = True
            panelSearch.Visible = False
            slideOccupancy.Enabled = True
            slidePayment.Enabled = True
        End If

    End Sub

    Private Sub listSearchList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles listSearchList.DoubleClick
        If searchmode = "Assessment" Then
            btnAssessmentFind.Enabled = True
            btnAssessmentMerge.Enabled = False
            btnAssessmentClose.Text = "&Close"
            btnAssessmentNew.Text = "&New"
            lblAdd.Visible = False
            txtAssessAdditional.Visible = False
            loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
            Call clearAllText(panelInnerSearch)
            listSearchList.Items.Clear()
            cboSearchBRGY.SelectedIndex = -1
            cboSearchSub.SelectedIndex = -1
            btnSearchClose.Text = "Close"
            slideAssessment.Enabled = True
            panelSearch.Visible = False
            btnAssessmentClose.Text = "&Cancel"
            btnAssessmentEdit.Enabled = True
        ElseIf searchmode = "Occupancy" Then
            loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
            panelSearch.Visible = False
            btnOccClose.Text = "&Cancel"
            slideOccupancy.Enabled = True
            Call clearAllText(panelInnerSearch)
            listSearchList.Items.Clear()
            cboSearchBRGY.SelectedIndex = -1
            cboSearchSub.SelectedIndex = -1
            btnSearchClose.Text = "Close"
            Call readOnly2Mode(PanelEx35)
        ElseIf searchmode = "Payment" Then
            loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
            panelSearch.Visible = False
            slidePayment.Enabled = True
            Call clearAllText(panelInnerSearch)
            listSearchList.Items.Clear()
            cboSearchBRGY.SelectedIndex = -1
            cboSearchSub.SelectedIndex = -1
        End If

    End Sub

    Private Sub listSearchList_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listSearchList.KeyUp
        If e.KeyCode = Keys.Enter Then
            If searchmode = "Assessment" Then
                btnAssessmentFind.Enabled = True
                btnAssessmentMerge.Enabled = False
                btnAssessmentClose.Text = "&Close"
                btnAssessmentNew.Text = "&New"
                lblAdd.Visible = False
                txtAssessAdditional.Visible = False
                loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
                Call clearAllText(panelInnerSearch)
                listSearchList.Items.Clear()
                cboSearchBRGY.SelectedIndex = -1
                cboSearchSub.SelectedIndex = -1
                btnSearchClose.Text = "Close"
                slideAssessment.Enabled = True
                panelSearch.Visible = False
                btnAssessmentClose.Text = "&Cancel"
            ElseIf searchmode = "Occupancy" Then
                loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
                panelSearch.Visible = False
                btnOccClose.Text = "&Cancel"
                slideOccupancy.Enabled = True
                Call clearAllText(panelInnerSearch)
                listSearchList.Items.Clear()
                cboSearchBRGY.SelectedIndex = -1
                cboSearchSub.SelectedIndex = -1
                btnSearchClose.Text = "Close"
            ElseIf searchmode = "Payment" Then
                loadSearchItem(listSearchList.FocusedItem.SubItems(0).Text)
                panelSearch.Visible = False
                slidePayment.Enabled = True
                Call clearAllText(panelInnerSearch)
                listSearchList.Items.Clear()
                cboSearchBRGY.SelectedIndex = -1
                cboSearchSub.SelectedIndex = -1
            End If

        End If
    End Sub

    Sub loadSearchItem(ByVal acn As String)
        Call loadBarangaysAssessment()
        Call loadSubdivisionsAssessment()
        Dim q As String = "SELECT tblSum.ACN, Date, tblApp.LastName, tblApp.FirstName, " & _
        "tblApp.MiddleName, Project, Lot, Block, Phase, " & _
        "SubdivisionCode, OtherInfo, Zone, BarangayCode, Remarks, PermitPre, PermitSub, " & _
        "PermitDate, Encoder, Additional, " & _
        "BuildConst, ElecInst, MechIns, PlumbIns, ElectroIns, BuildAcc, " & _
        "OtherAcc, BuildOcc, BuildInsp, CertFee, Fines, TOTALAssess, " & _
        "Local, Natl, OBO, CONCAT(tblUsers.FirstName,' ',tblUsers.LastName), tblApp.Status " & _
        "FROM tblassessmentapplicant tblApp " & _
        "INNER JOIN tblassessmentsummary tblSum " & _
        "ON tblApp.ACn = tblSum.ACN " & _
        "INNER JOIN tblUsers " & _
        "ON tblUsers.UserID = tblApp.Encoder " & _
        "WHERE tblApp.ACN = '" & initializeQueryEntry(acn, False) & "'"
        Dim dtSearchItem As DataTable = executeQuery(q, "tblSearchResult")
        If dtSearchItem.Rows.Count > 0 Then
            Dim subDiv As String
            If dtSearchItem.Rows(0)(9).ToString = "" Then
                subDiv = "0"
            Else
                subDiv = dtSearchItem.Rows(0)(9).ToString
            End If
            If searchmode = "Assessment" Then
                txtACN.Text = dtSearchItem.Rows(0)(0).ToString
                dtAssessDate.Value = CDate(dtSearchItem.Rows(0)(1).ToString)
                txtAssessLast.Text = dtSearchItem.Rows(0)(2).ToString
                txtAssessFirst.Text = dtSearchItem.Rows(0)(3).ToString
                txtAssessMiddle.Text = dtSearchItem.Rows(0)(4).ToString
                txtAssessProject.Text = dtSearchItem.Rows(0)(5).ToString
                txtAssessLot.Text = dtSearchItem.Rows(0)(6).ToString
                txtAssessBlock.Text = dtSearchItem.Rows(0)(7).ToString
                txtAssessPhase.Text = dtSearchItem.Rows(0)(8).ToString
                cboSubdivision_Assess.SelectedIndex = CInt(subDiv) - 1
                txtAssessOther.Text = dtSearchItem.Rows(0)(10).ToString
                txtAssessZone.Text = dtSearchItem.Rows(0)(11).ToString
                cbo_Barangay_Assess.SelectedIndex = CInt(dtSearchItem.Rows(0)(12).ToString) - 1
                txtAssessRemarks.Text = dtSearchItem.Rows(0)(13).ToString
                txtAssessPermitPre.Text = dtSearchItem.Rows(0)(14).ToString
                txtAssessPermitSub.Text = dtSearchItem.Rows(0)(15).ToString
                dtPermitIssue.Value = CDate(dtSearchItem.Rows(0)(16).ToString)
                txtAssesCode.Text = dtSearchItem.Rows(0)(17).ToString
                txtAssessAdditional.Text = dtSearchItem.Rows(0)(18).ToString
                txtBuildConstFee.Text = FormatNumber(dtSearchItem.Rows(0)(19).ToString, 2)
                txtElecInsFee.Text = FormatNumber(dtSearchItem.Rows(0)(20).ToString, 2)
                txtMechInsFee.Text = FormatNumber(dtSearchItem.Rows(0)(21).ToString, 2)
                txtPlumbInsFee.Text = FormatNumber(dtSearchItem.Rows(0)(22).ToString, 2)
                txtElectroInsFee.Text = FormatNumber(dtSearchItem.Rows(0)(23).ToString, 2)
                txtBuildAccFee.Text = FormatNumber(dtSearchItem.Rows(0)(24).ToString, 2)
                txtOthrAccFee.Text = FormatNumber(dtSearchItem.Rows(0)(25).ToString, 2)
                txtBuildOccFee.Text = FormatNumber(dtSearchItem.Rows(0)(26).ToString, 2)
                txtBuildInspFee.Text = FormatNumber(dtSearchItem.Rows(0)(27).ToString, 2)
                txtCertFee.Text = FormatNumber(dtSearchItem.Rows(0)(28).ToString, 2)
                txtFinesFee.Text = FormatNumber(dtSearchItem.Rows(0)(29).ToString, 2)
                txtTotalAssessFee.Text = FormatNumber(dtSearchItem.Rows(0)(30).ToString, 2)
                txtLocalGov.Text = FormatNumber(dtSearchItem.Rows(0)(31).ToString, 2)
                txtNatlGov.Text = FormatNumber(dtSearchItem.Rows(0)(32).ToString, 2)
                txtOBO.Text = FormatNumber(dtSearchItem.Rows(0)(33).ToString, 2)
                txtAssessUser.Text = dtSearchItem.Rows(0)(34).ToString
                txtAssessStatus.Text = dtSearchItem.Rows(0)(35).ToString
                If dtSearchItem.Rows(0)(18).ToString <> "NA" Then 'Additional ACN
                    lblAdd.Visible = True
                    txtAssessAdditional.Visible = True
                    btnAssessmentAdditional.Enabled = False
                    btnAssessmentMerge.Enabled = False
                Else 'Parent ACN
                    If usertype <> "1" Then
                        btnAssessmentAdditional.Enabled = True
                        If checkAdditionals(dtSearchItem.Rows(0)(0).ToString) Then
                            btnAssessmentMerge.Enabled = True
                        End If
                    End If
                End If
            ElseIf searchmode = "Occupancy" Then
                txtOccL.Text = dtSearchItem.Rows(0)(2).ToString
                txtOccF.Text = dtSearchItem.Rows(0)(3).ToString
                txtOccM.Text = dtSearchItem.Rows(0)(4).ToString
                txtOccLot.Text = dtSearchItem.Rows(0)(6).ToString
                txtOccBlock.Text = dtSearchItem.Rows(0)(7).ToString
                txtOccPh.Text = dtSearchItem.Rows(0)(8).ToString
                cboOccSubd.SelectedIndex = CInt(subDiv) - 1
                txtOccSt.Text = dtSearchItem.Rows(0)(10).ToString
                txtOccZone.Text = dtSearchItem.Rows(0)(11).ToString
                cboOccBrgy.SelectedIndex = CInt(dtSearchItem.Rows(0)(12).ToString) - 1
            ElseIf searchmode = "Payment" Then
                txtPaymentACN.Text = dtSearchItem.Rows(0)(0).ToString
                txtPaymentProject.Text = dtSearchItem.Rows(0)(5).ToString
                txtPaymentL.Text = dtSearchItem.Rows(0)(2).ToString
                txtPaymentF.Text = dtSearchItem.Rows(0)(3).ToString
                txtPaymentM.Text = dtSearchItem.Rows(0)(4).ToString
                txtPaymentLot.Text = dtSearchItem.Rows(0)(6).ToString
                txtPaymentBlock.Text = dtSearchItem.Rows(0)(7).ToString
                txtPaymentPhase.Text = dtSearchItem.Rows(0)(8).ToString
                cboPaymentSub.SelectedIndex = CInt(subDiv) - 1
                txtPaymentStreet.Text = dtSearchItem.Rows(0)(10).ToString
                txtPaymentZone.Text = dtSearchItem.Rows(0)(11).ToString
                cboPaymentBgy.SelectedIndex = CInt(dtSearchItem.Rows(0)(12).ToString) - 1
                txtPaymentAssess.Text = FormatNumber(dtSearchItem.Rows(0)(30).ToString, 2)

                txtFee1.Text = FormatNumber(dtSearchItem.Rows(0)(19).ToString, 2)
                txtFee2.Text = FormatNumber(dtSearchItem.Rows(0)(20).ToString, 2)
                txtFee3.Text = FormatNumber(dtSearchItem.Rows(0)(21).ToString, 2)
                txtFee4.Text = FormatNumber(dtSearchItem.Rows(0)(22).ToString, 2)
                txtFee5.Text = FormatNumber(dtSearchItem.Rows(0)(23).ToString, 2)
                txtFee6.Text = FormatNumber(dtSearchItem.Rows(0)(24).ToString, 2)
                txtFee7.Text = FormatNumber(dtSearchItem.Rows(0)(25).ToString, 2)
                txtFee8.Text = FormatNumber(dtSearchItem.Rows(0)(26).ToString, 2)
                txtFee9.Text = FormatNumber(dtSearchItem.Rows(0)(27).ToString, 2)
                txtFee10.Text = FormatNumber(dtSearchItem.Rows(0)(28).ToString, 2)
                txtFee11.Text = FormatNumber(dtSearchItem.Rows(0)(29).ToString, 2)
            End If

        End If
    End Sub

    Function checkAdditionals(ByVal acn As String) As Boolean
        Dim q As String = "SELECT * FROM tblassessmentapplicant WHERE Additional = '" & initializeQueryEntry(acn, True) & "'"
        Dim dtCheckAdd As DataTable = executeQuery(q, "tblAdd")
        If dtCheckAdd.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub truncateMergeTable()
        Dim dtTruncate As DataTable = executeQuery("truncate tblassessmentmerge", "tblTruncate")
    End Sub

    Sub mergeDataPerACN(ByVal ACN As String)
        Dim q As String = _
            "INSERT INTO tblassessmentmerge ( " & _
            "	ACN, " & _
            "	BuildConst, " & _
            "	ElecInst, " & _
            "	MechIns, " & _
            "	PlumbIns, " & _
            "	ElectroIns, " & _
            "	BuildAcc, " & _
            "	OtherAcc, " & _
            "	BuildOcc, " & _
            "	BuildInsp, " & _
            "	CertFee, " & _
            "	Fines, " & _
            "	TOTALAssess, " & _
            "	LOCAL, " & _
            "	Natl, " & _
            "	OBO " & _
            ") SELECT " & _
            "	'" & ACN & "', " & _
            "	BuildConst, " & _
            "	ElecInst, " & _
            "	MechIns, " & _
            "	PlumbIns, " & _
            "	ElectroIns, " & _
            "	BuildAcc, " & _
            "	OtherAcc, " & _
            "	BuildOcc, " & _
            "	BuildInsp, " & _
            "	CertFee, " & _
            "	Fines, " & _
            "	TOTALAssess, " & _
            "	LOCAL, " & _
            "	Natl, " & _
            "	OBO " & _
            "FROM " & _
            "	tblassessmentapplicant tblApp " & _
            "INNER JOIN tblassessmentsummary tblSum ON tblApp.ACn = tblSum.ACN " & _
            "WHERE " & _
            "	( " & _
            "		tblApp.ACn = '" & ACN & "' " & _
            "		AND tblApp.Additional = 'NA' " & _
            "	) " & _
            "OR ( " & _
            "	tblApp.ACn != '" & ACN & "' " & _
            "	AND tblApp.Additional = '" & ACN & "' " & _
            ")"
        Dim dtMerge As DataTable = executeQuery(q, "dtMerge")
    End Sub

    Sub fetchMergedData(ByVal acn As String)
        Dim q As String = _
            "SELECT " & _
            "	tblApp.ACN, " & _
            "	Date, " & _
            "	tblApp.LastName, " & _
            "	tblApp.FirstName, " & _
            "	tblApp.MiddleName, " & _
            "	Project, " & _
            "	Lot, " & _
            "	Block, " & _
            "	Phase, " & _
            "	SubdivisionCode, " & _
            "	OtherInfo, " & _
            "	Zone, " & _
            "	BarangayCode, " & _
            "	Remarks, " & _
            "	PermitPre, " & _
            "	PermitSub, " & _
            "	Encoder, " & _
            "	CONCAT(tblUsers.FirstName,' ',tblUsers.LastName), " & _
            "	Additional, " & _
            "	SUM(BuildConst), " & _
            "	SUM(ElecInst), " & _
            "	SUM(MechIns), " & _
            "	SUM(PlumbIns), " & _
            "	SUM(ElectroIns), " & _
            "	SUM(BuildAcc), " & _
            "	SUM(OtherAcc), " & _
            "	SUM(BuildOcc), " & _
            "	SUM(BuildInsp), " & _
            "	SUM(CertFee), " & _
            "	SUM(Fines), " & _
            "	SUM(TOTALAssess), " & _
            "	SUM(LOCAL), " & _
            "	SUM(Natl), " & _
            "	SUM(OBO), " & _
            "   PermitDate " & _
            "FROM " & _
            "	tblassessmentmerge tblMerge " & _
            "INNER JOIN tblassessmentapplicant tblApp ON tblApp.ACN = tblMerge.ACN " & _
            "INNER JOIN tblUsers ON tblUsers.UserID = tblApp.Encoder " & _
            "WHERE " & _
            "	tblMerge.ACN = '" & acn & "' " & _
            "GROUP BY " & _
            "	tblMerge.ACN"
        Dim dtMergeData As DataTable = executeQuery(q, "tblMergeData")
        If dtMergeData.Rows.Count > 0 Then
            Dim subDiv As String
            If dtMergeData.Rows(0)(9).ToString = "" Then
                subDiv = "0"
            Else
                subDiv = dtMergeData.Rows(0)(9).ToString
            End If
            txtACN.Text = dtMergeData.Rows(0)(0).ToString
            dtAssessDate.Value = CDate(dtMergeData.Rows(0)(1).ToString)
            txtAssessLast.Text = dtMergeData.Rows(0)(2).ToString
            txtAssessFirst.Text = dtMergeData.Rows(0)(3).ToString
            txtAssessMiddle.Text = dtMergeData.Rows(0)(4).ToString
            txtAssessProject.Text = dtMergeData.Rows(0)(5).ToString
            txtAssessLot.Text = dtMergeData.Rows(0)(6).ToString
            txtAssessBlock.Text = dtMergeData.Rows(0)(7).ToString
            txtAssessPhase.Text = dtMergeData.Rows(0)(8).ToString
            cboSubdivision_Assess.SelectedIndex = CInt(subDiv) - 1
            txtAssessOther.Text = dtMergeData.Rows(0)(10).ToString
            txtAssessZone.Text = dtMergeData.Rows(0)(11).ToString
            cbo_Barangay_Assess.SelectedIndex = CInt(dtMergeData.Rows(0)(12).ToString) - 1
            txtAssessPermitPre.Text = dtMergeData.Rows(0)(14).ToString
            txtAssessPermitSub.Text = dtMergeData.Rows(0)(15).ToString
            txtAssesCode.Text = dtMergeData.Rows(0)(16).ToString
            txtAssessAdditional.Text = dtMergeData.Rows(0)(18).ToString
            txtBuildConstFee.Text = FormatNumber(dtMergeData.Rows(0)(19).ToString, 2)
            txtElecInsFee.Text = FormatNumber(dtMergeData.Rows(0)(20).ToString, 2)
            txtMechInsFee.Text = FormatNumber(dtMergeData.Rows(0)(21).ToString, 2)
            txtPlumbInsFee.Text = FormatNumber(dtMergeData.Rows(0)(22).ToString, 2)
            txtElectroInsFee.Text = FormatNumber(dtMergeData.Rows(0)(23).ToString, 2)
            txtBuildAccFee.Text = FormatNumber(dtMergeData.Rows(0)(24).ToString, 2)
            txtOthrAccFee.Text = FormatNumber(dtMergeData.Rows(0)(25).ToString, 2)
            txtBuildOccFee.Text = FormatNumber(dtMergeData.Rows(0)(26).ToString, 2)
            txtBuildInspFee.Text = FormatNumber(dtMergeData.Rows(0)(27).ToString, 2)
            txtCertFee.Text = FormatNumber(dtMergeData.Rows(0)(28).ToString, 2)
            txtFinesFee.Text = FormatNumber(dtMergeData.Rows(0)(29).ToString, 2)
            txtTotalAssessFee.Text = FormatNumber(dtMergeData.Rows(0)(30).ToString, 2)
            txtLocalGov.Text = FormatNumber(dtMergeData.Rows(0)(31).ToString, 2)
            txtNatlGov.Text = FormatNumber(dtMergeData.Rows(0)(32).ToString, 2)
            txtOBO.Text = FormatNumber(dtMergeData.Rows(0)(33).ToString, 2)
            txtAssessUser.Text = dtMergeData.Rows(0)(17).ToString
            txtAssessRemarks.Text = "This is a MERGED RECORD." & vbNewLine & _
                    "A Merged Record is a temporary record and is available only for viewing."
            dtPermitIssue.Text = CDate(dtMergeData.Rows(0)(34).ToString)
            btnAssessmentAdditional.Enabled = False
            txtAssessStatus.Visible = False
            lblAssessStatus.Visible = False
        End If
    End Sub

    Private Sub btnAssessmentMerge_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentMerge.Click
        Call truncateMergeTable()
        Call mergeDataPerACN(txtACN.Text)
        Call fetchMergedData(txtACN.Text)
        Call logtrail(userID, "Merge", "Assessment", txtACN.Text.Trim)
    End Sub


    Private Sub btnAssessmentAdditional_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentAdditional.Click
        txtAssessAdditional.Text = txtACN.Text
        txtAssessAdditional.Visible = True
        lblAdd.Visible = True
        Call resetZeroAllText(panelAssessSum)
        Call writeAllText(panelAssessSum)
        txtAssessRemarks.Text = "Additional Assessment for " & txtAssessAdditional.Text
        txtAssessRemarks.ReadOnly = False
        Call generateACN()
        dtAssessDate.Value = Today
        txtAssesCode.Text = userID.ToString.PadLeft(3, "0")
        txtAssessUser.Text = loggedUserName
        btnAssessmentFind.Enabled = False
        btnAssessmentMerge.Enabled = False
        btnAssessmentAdditional.Enabled = False
        btnAssessmentNew.Text = "&Save"
        btnAssessmentClose.Text = "&Cancel"
        txtAssessRemarks.Focus()
        additionalAssessment = True
        dtAssessDate.Enabled = True
        txtAssessStatus.Text = "Pending"
    End Sub

    Private Sub btnSummaryDetails_Click(sender As System.Object, e As System.EventArgs) Handles btnSummaryDetails.Click
        If CDate(dtToSummary.Value.ToShortDateString) < CDate(dtFromSummary.Value.ToShortDateString) Then
            MessageBoxEx.Show("Invalid period covered. Please check date values.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim query As String = _
            "SELECT " & _
            "	tblAssessApp.ACN, " & _
            "	Date, " & _
            "	tblAssessApp.LastName, " & _
            "	tblAssessApp.FirstName, " & _
            "	tblAssessApp.MiddleName, " & _
            "	Project, " & _
            "	Lot, " & _
            "	Block, " & _
            "	Phase, " & _
            "	SubdName, " & _
            "	OtherInfo, " & _
            "	Zone, " & _
            "	BrgyName, " & _
            "	Remarks, " & _
            "	CONCAT(PermitPre, '-', PermitSub), " & _
            "	PermitDate, " & _
            "	TOTALAssess, " & _
            "	Additional, " & _
            "	CONCAT(Encoder, ' - ', Username) Encoder, " & _
            "	PermitPre, " & _
            "	PermitSub " & _
            "FROM " & _
            "	tblassessmentapplicant tblAssessApp " & _
            "LEFT JOIN tblassessmentsummary tblAssessSum ON tblAssessApp.ACN = tblAssessSum.ACN " & _
            "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
            "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID " & _
            "LEFT JOIN tblUsers ON Encoder = UserID " & _
            "WHERE " & _
            "	STR_TO_DATE(DATE, '%m/%d/%Y') BETWEEN STR_TO_DATE('" & dtFromSummary.Value.ToShortDateString & "', '%m/%d/%Y') " & _
            "AND STR_TO_DATE('" & dtToSummary.Value.ToShortDateString & "', '%m/%d/%Y')"
        Dim dtDetail As DataTable = executeQuery(query, "tblDetails")
        If dtDetail.Rows.Count > 0 Then
            listSummary.Items.Clear()
            For reccount As Integer = 0 To dtDetail.Rows.Count - 1
                Dim listItem As ListViewItem = listSummary.Items.Add(dtDetail.Rows(reccount)(0).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(2).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(3).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(4).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(5).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(6).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(7).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(8).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(9).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(10).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(11).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(12).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(1).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(14).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(15).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(18).ToString)
                listItem.SubItems.Add(dtDetail.Rows(reccount)(17).ToString)
                listItem.SubItems.Add(FormatNumber(dtDetail.Rows(reccount)(16).ToString, 2))
            Next
        End If
        listSummary.Visible = True
        panelSummaryParent.Visible = False
        lblSummary.Text = "Applicant and Payment Details"
        Call logtrail(userID, "Payment Details", "Payment Summary", "***")
    End Sub

    Private Sub btnSummary_Click(sender As System.Object, e As System.EventArgs) Handles btnSummary.Click
        If CDate(dtToSummary.Value.ToShortDateString) < CDate(dtFromSummary.Value.ToShortDateString) Then
            MessageBoxEx.Show("Invalid period covered. Please check date values.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim q As String = _
            "SELECT " & _
            "	SUM(BuildConst), " & _
            "	SUM(ElecInst), " & _
            "	SUM(MechIns), " & _
            "	SUM(PlumbIns), " & _
            "	SUM(ElectroIns), " & _
            "	SUM(BuildAcc), " & _
            "	SUM(OtherAcc), " & _
            "	SUM(BuildOcc), " & _
            "	SUM(BuildInsp), " & _
            "	SUM(CertFee), " & _
            "	SUM(Fines), " & _
            "	SUM(TOTALAssess), " & _
            "	SUM(LOCAL), " & _
            "	SUM(Natl), " & _
            "	SUM(OBO) " & _
            "FROM " & _
            "	tblassessmentsummary tblSum " & _
            "INNER JOIN tblassessmentapplicant tblApp ON tblSum.ACN = tblApp.acn " & _
            "WHERE " & _
            "	(STR_TO_DATE(DATE, '%m/%d/%Y') BETWEEN STR_TO_DATE('" & dtFromSummary.Value.ToShortDateString & "', '%m/%d/%Y') " & _
            "AND STR_TO_DATE('" & dtToSummary.Value.ToShortDateString & "', '%m/%d/%Y')) AND tblApp.Status = 'Paid'"
        Dim dtSum As DataTable = executeQuery(q, "tblDetails")
        If dtSum.Rows.Count > 0 Then
            txtA1.Text = FormatNumber(IIf(dtSum.Rows(0)(0).ToString = "", "0", dtSum.Rows(0)(0).ToString), 2)
            txtA2.Text = FormatNumber(IIf(dtSum.Rows(0)(1).ToString = "", "0", dtSum.Rows(0)(1).ToString), 2)
            txtA3.Text = FormatNumber(IIf(dtSum.Rows(0)(2).ToString = "", "0", dtSum.Rows(0)(2).ToString), 2)
            txtA4.Text = FormatNumber(IIf(dtSum.Rows(0)(3).ToString = "", "0", dtSum.Rows(0)(3).ToString), 2)
            txtA5.Text = FormatNumber(IIf(dtSum.Rows(0)(4).ToString = "", "0", dtSum.Rows(0)(4).ToString), 2)
            txtA6.Text = FormatNumber(IIf(dtSum.Rows(0)(5).ToString = "", "0", dtSum.Rows(0)(5).ToString), 2)
            txtA7.Text = FormatNumber(IIf(dtSum.Rows(0)(6).ToString = "", "0", dtSum.Rows(0)(6).ToString), 2)
            txtA8.Text = FormatNumber(IIf(dtSum.Rows(0)(7).ToString = "", "0", dtSum.Rows(0)(7).ToString), 2)
            txtA9.Text = FormatNumber(IIf(dtSum.Rows(0)(8).ToString = "", "0", dtSum.Rows(0)(8).ToString), 2)
            txtA10.Text = FormatNumber(IIf(dtSum.Rows(0)(9).ToString = "", "0", dtSum.Rows(0)(9).ToString), 2)
            txtA11.Text = FormatNumber(IIf(dtSum.Rows(0)(10).ToString = "", "0", dtSum.Rows(0)(10).ToString), 2)
            txtA12.Text = FormatNumber(IIf(dtSum.Rows(0)(11).ToString = "", "0", dtSum.Rows(0)(11).ToString), 2)
            txtA13.Text = FormatNumber(IIf(dtSum.Rows(0)(12).ToString = "", "0", dtSum.Rows(0)(12).ToString), 2)
            txtA14.Text = FormatNumber(IIf(dtSum.Rows(0)(13).ToString = "", "0", dtSum.Rows(0)(13).ToString), 2)
            txtA15.Text = FormatNumber(IIf(dtSum.Rows(0)(14).ToString = "", "0", dtSum.Rows(0)(14).ToString), 2)
        End If
        listSummary.Visible = False
        panelSummaryParent.Visible = True
        lblSummary.Text = "Summary of Payments"
        Call logtrail(userID, "Summary", "Payment Summary", "***")
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Call clearAllInput(panelPaymentSummary)
        listSummary.Items.Clear()
        listSummary.Visible = False
        panelSummaryParent.Visible = True
        lblSummary.Text = "Summary of Payments"
        slideSummary.IsOpen = False
        expSummary.Expanded = False
        panelSideBar.Enabled = True
        picSummary.Image = My.Resources.Icon_Calcu
        expSummary.Expanded = False
        expSummary.TitleStyle.ForeColor.Color = Color.White
        expSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelSummary.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
    End Sub


    Private Sub btnOccClose_Click(sender As System.Object, e As System.EventArgs) Handles btnOccClose.Click
        If btnOccClose.Text = "&Cancel" Then
            Call clearAllText(PanelEx35)
            Call readAllText(PanelEx35)
            btnOccuAdd.Text = "&New"
            btnOccFind.Enabled = True
            btnOccClose.Text = "&Close"
            btnSearchApplicantData.Enabled = False
        Else
            slideOccupancy.IsOpen = False
            panelSideBar.Enabled = True
            picOccupancy.Image = My.Resources.Icon_Occupancy
            expOccupancy.Expanded = False
            expOccupancy.TitleStyle.ForeColor.Color = Color.White
            expOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
            picPanelOccupancy.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        End If
    End Sub

    Private Sub btnOccuAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnOccuAdd.Click
        If btnOccuAdd.Text = "&New" Then
            btnOccuAdd.Text = "&Save"
            btnOccFind.Enabled = False
            btnOccClose.Text = "&Cancel"
            Call writeAllText(PanelEx35)
            Call clearAllText(PanelEx35)
            btnSearchApplicantData.Enabled = True
            Call generateOCN()
            txtOccCity.Text = "City of Parañaque"
            txtOccL.Focus()
            Call loadBarangaysAssessment2()
            Call loadSubdivisionsAssessment2()
        Else
            If txtOccL.Text.Trim = "" Then
                MessageBoxEx.Show("Family name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccL.Focus()
                Exit Sub
            End If
            If txtOccF.Text.Trim = "" Then
                MessageBoxEx.Show("First name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccF.Focus()
                Exit Sub
            End If
            If txtOccOwnAdd.Text.Trim = "" Then
                MessageBoxEx.Show("Owner address is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccOwnAdd.Focus()
                Exit Sub
            End If
            If cboOccBrgy.SelectedIndex = -1 Then
                MessageBoxEx.Show("Barangay is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cboOccBrgy.Focus()
                Exit Sub
            End If
            If cboOccBuild.SelectedIndex = -1 Then
                MessageBoxEx.Show("Use or Type of Occupancy is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cboOccBuild.Focus()
                Exit Sub
            End If
            If txtOccOwnAdd.Text.Trim = "" Then
                MessageBoxEx.Show("Owner address is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccOwnAdd.Focus()
                Exit Sub
            End If
            If txtOccAreaP.Text.Trim = "" Then
                MessageBoxEx.Show("Total floor area(estimated) is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccAreaP.Focus()
                Exit Sub
            End If
            If txtOccAreaP.Text.Trim = "" Then
                MessageBoxEx.Show("Total floor area(actual) is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccAreaP.Focus()
                Exit Sub
            End If
            If txtOccStorey.Text.Trim = "" Then
                MessageBoxEx.Show("No. of storey is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccStorey.Focus()
                Exit Sub
            End If
            If txtOccCost.Text.Trim = "" Then
                MessageBoxEx.Show("Estimated cost is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtOccCost.Focus()
                Exit Sub
            End If
            Dim subdivision As String
            If cboOccSubd.SelectedIndex = -1 Then
                subdivision = ""
            Else
                subdivision = subdAssessment(cboOccSubd.SelectedIndex)
            End If
            Dim q As String = _
                "INSERT INTO tbloccupancy ( " & _
                "	OCN, " & _
                "	Date, " & _
                "	Lastname, " & _
                "	FirstName, " & _
                "	MiddleName, " & _
                "	OwnerAddress, " & _
                "	Lot, " & _
                "	Block, " & _
                "	Phase, " & _
                "	SubdivisionCode, " & _
                "	OtherInfo, " & _
                "	Zone, " & _
                "	Barangay, " & _
                "	OccupancyCode, " & _
                "	StartDatePro, " & _
                "	StartDateAct, " & _
                "	CompleteDatePro, " & _
                "	CompleteDateAct, " & _
                "	TotalAreaEst, " & _
                "	TotalAreaAct, " & _
                "	Storeys, " & _
                "	EstCost " & _
                ") " & _
                "VALUES( " & _
                "	'" & initializeQueryEntry(txtOCN.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(dtOCNDate.Value.ToShortDateString, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccL.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccF.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccM.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccOwnAdd.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccLot.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccBlock.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccPh.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(subdivision, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccSt.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccZone.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(brgyAssessment(cboOccBrgy.SelectedIndex), False) & "', " & _
                "	'" & initializeQueryEntry(classAssessment(cboOccBuild.SelectedIndex), False) & "', " & _
                "	'" & initializeQueryEntry(dtOccConstP.Value.ToShortDateString, False) & "', " & _
                "	'" & initializeQueryEntry(dtOccConstA.Value.ToShortDateString, False) & "', " & _
                "	'" & initializeQueryEntry(dtOccComP.Value.ToShortDateString, False) & "', " & _
                "	'" & initializeQueryEntry(dtOccComA.Value.ToShortDateString, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccAreaP.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccAreaA.Text.Trim, False) & "', " & _
                "	'" & initializeQueryEntry(txtOccStorey.Text.Trim, False) & "', " & _
                "	'" & CDbl(txtOccCost.Text.Trim) & "' " & _
                ")"
            executeQuery(q, "tblInsertOcc")
            MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call logtrail(userID, "Add", "Occupancy", txtOCN.Text.Trim)
            btnOccuAdd.Text = "&New"
            btnOccFind.Enabled = True
            btnOccClose.Text = "&Close"
            Call clearAllText(PanelEx35)
            Call readAllText(PanelEx35)
            btnSearchApplicantData.Enabled = False
        End If
    End Sub

    Private Sub btnSearchApplicantData_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchApplicantData.Click
        slideOccupancy.Enabled = False
        panelSearch.Visible = True
        searchmode = "Occupancy"
    End Sub

    Private Sub txtOccCost_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOccCost.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOccCost.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtOccCost_Leave(sender As System.Object, e As System.EventArgs) Handles txtOccCost.Leave
        If txtOccCost.Text.Trim = "" Then
            txtOccCost.Text = FormatNumber(0, 2)
        Else
            txtOccCost.Text = FormatNumber(txtOccCost.Text, 2)
        End If

    End Sub

    Sub searchOccupancy()
        Dim subdivision As String
        If cboOccSSub.SelectedIndex = -1 Then
            subdivision = ""
        Else
            subdivision = subdAssessment(cboOccSSub.SelectedIndex)
        End If
        Dim brgay As String
        If cboOccSBrgy.SelectedIndex = -1 Then
            brgay = ""
        Else
            brgay = subdAssessment(cboOccSBrgy.SelectedIndex)
        End If
        Dim build As String
        If cboOccSBuild.SelectedIndex = -1 Then
            build = ""
        Else
            build = classAssessment(cboOccSBuild.SelectedIndex)
        End If
        Dim q As String = "SELECT " & _
                    "	OCN, " & _
                    "	Date, " & _
                    "	Lastname, " & _
                    "	FirstName, " & _
                    "	MiddleName, " & _
                    "	OwnerAddress, " & _
                    "	Lot, " & _
                    "	Block, " & _
                    "	Phase, " & _
                    "	CONCAT(SubdID,' - ',SubdName) SubdivisionCode, " & _
                    "	OtherInfo, " & _
                    "	Zone, " & _
                    "	CONCAT(BrgyID,' - ',BrgyName) Barangay, " & _
                    "	CONCAT('[',Division,'] ', GenClass) OccupancyCode, " & _
                    "	StartDatePro, " & _
                    "	StartDateAct, " & _
                    "	CompleteDatePro, " & _
                    "	CompleteDateAct, " & _
                    "	TotalAreaEst, " & _
                    "	TotalAreaAct, " & _
                    "	Storeys, " & _
                    "	EstCost " & _
                    "FROM tbloccupancy tblAssessOcc " & _
                    "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
                    "LEFT JOIN tblbarangays tblSubd ON Barangay = brgyID " & _
                    "LEFT JOIN tblclassifications tblClass ON ClassID = OccupancyCode " & _
                    "WHERE LastName LIKE '" & initializeQueryEntry(txtOccSL.Text.Trim, False) & "%' " & _
                    "AND FirstName LIKE '" & initializeQueryEntry(txtOccSF.Text.Trim, False) & "%' " & _
                    "AND MiddleName LIKE '" & initializeQueryEntry(txtOccSM.Text.Trim, False) & "%' " & _
                    "AND OwnerAddress LIKE '" & initializeQueryEntry(txtOccSAddress.Text.Trim, False) & "%' " & _
                    "AND Lot LIKE '" & initializeQueryEntry(txtOccSLot.Text.Trim, False) & "%' " & _
                    "AND Block LIKE '" & initializeQueryEntry(txtOccSBlock.Text.Trim, False) & "%' " & _
                    "AND Phase LIKE '" & initializeQueryEntry(txtOccSPh.Text.Trim, False) & "%' " & _
                    "AND SubDivisionCode LIKE '" & initializeQueryEntry(subdivision, False) & "%' " & _
                    "AND OtherInfo LIKE '" & initializeQueryEntry(txtOccSStreet.Text.Trim, False) & "%' " & _
                    "AND Zone LIKE '" & initializeQueryEntry(txtOccSZone.Text.Trim, False) & "%' " & _
                    "AND Barangay LIKE '" & initializeQueryEntry(brgay, False) & "%' " & _
                    "AND OccupancyCode LIKE '" & initializeQueryEntry(build, False) & "%' "
        Dim dtSearch As DataTable = executeQuery(q, "tblSearchOcc")
        If dtSearch.Rows.Count > 0 Then
            btnOccSearchCancel.Text = "Clear"
            listOccSearch.Items.Clear()
            For reccount As Integer = 0 To dtSearch.Rows.Count - 1
                Dim listItem As ListViewItem = listOccSearch.Items.Add(dtSearch.Rows(reccount)(0).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(2).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(3).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(4).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(5).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(6).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(7).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(8).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(9).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(10).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(11).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(12).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(13).ToString)
                listItem.SubItems.Add(dtSearch.Rows(reccount)(1).ToString)
                listItem.SubItems.Add(FormatNumber(dtSearch.Rows(reccount)(21).ToString, 2))
            Next
        Else
            btnOccSearchCancel.Text = "Close"
            listOccSearch.Items.Clear()
        End If
    End Sub

    Private Sub btnOccSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnOccSearch.Click
        Call searchOccupancy()
    End Sub

    Private Sub btnOccFind_Click(sender As System.Object, e As System.EventArgs) Handles btnOccFind.Click
        slideOccupancy.Enabled = False
        panelOccSearch.Visible = True
    End Sub

    Private Sub btnOccSearchCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnOccSearchCancel.Click
        If btnOccSearchCancel.Text = "Clear" Then
            btnOccSearchCancel.Text = "Close"
            listOccSearch.Items.Clear()
            Call clearAllText(PanelEx42)
        Else
            slideOccupancy.Enabled = True
            panelOccSearch.Visible = False
        End If
    End Sub

    Private Sub listOccSearch_DoubleClick(sender As System.Object, e As System.EventArgs) Handles listOccSearch.DoubleClick
        btnOccFind.Enabled = True
        Call loadOccSerachItem(listOccSearch.FocusedItem.SubItems(0).Text)
        btnOccSearchCancel.Text = "Close"
        btnOccClose.Text = "&Cancel"
        listOccSearch.Items.Clear()
        Call clearAllText(PanelEx42)
        slideOccupancy.Enabled = True
        panelOccSearch.Visible = False
    End Sub

    Private Sub listOccSearch_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles listOccSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            btnOccFind.Enabled = True
            Call loadOccSerachItem(listOccSearch.FocusedItem.SubItems(0).Text)
            btnOccSearchCancel.Text = "Close"
            btnOccClose.Text = "&Cancel"
            listOccSearch.Items.Clear()
            Call clearAllText(PanelEx42)
            slideOccupancy.Enabled = True
            panelOccSearch.Visible = False
        End If
    End Sub

    Sub loadOccSerachItem(ByVal ocn As String)
        Call loadBarangaysAssessment()
        Call loadSubdivisionsAssessment()
        Dim q As String = _
            "SELECT " & _
            "	OCN, " & _
            "	Date, " & _
            "	Lastname, " & _
            "	FirstName, " & _
            "	MiddleName, " & _
            "	OwnerAddress, " & _
            "	Lot, " & _
            "	Block, " & _
            "	Phase, " & _
            "	SubdivisionCode, " & _
            "	OtherInfo, " & _
            "	Zone, " & _
            "	Barangay, " & _
            "	OccupancyCode, " & _
            "	StartDatePro, " & _
            "	StartDateAct, " & _
            "	CompleteDatePro, " & _
            "	CompleteDateAct, " & _
            "	TotalAreaEst, " & _
            "	TotalAreaAct, " & _
            "	Storeys, " & _
            "	EstCost " & _
            "FROM " & _
            "	tbloccupancy " & _
            "WHERE " & _
            "	OCN = '" & ocn & "'"
        Dim dt As DataTable = executeQuery(q, "tblSearchOccItem")
        If dt.Rows.Count > 0 Then
            Dim subdi As String
            If dt.Rows(0)(9).ToString = "" Then
                subdi = "0"
            Else
                subdi = dt.Rows(0)(9).ToString()
            End If
            txtOCN.Text = dt.Rows(0)(0).ToString()
            dtOCNDate.Value = CDate(dt.Rows(0)(1).ToString())
            txtOccL.Text = dt.Rows(0)(2).ToString()
            txtOccF.Text = dt.Rows(0)(3).ToString()
            txtOccM.Text = dt.Rows(0)(4).ToString()
            txtOccOwnAdd.Text = dt.Rows(0)(5).ToString()
            txtOccLot.Text = dt.Rows(0)(6).ToString()
            txtOccBlock.Text = dt.Rows(0)(7).ToString()
            txtOccPh.Text = dt.Rows(0)(8).ToString()
            cboOccSubd.SelectedIndex = CInt(subdi) - 1
            txtOccSt.Text = dt.Rows(0)(10).ToString()
            txtOccZone.Text = dt.Rows(0)(11).ToString()
            cboOccBrgy.SelectedIndex = CInt(dt.Rows(0)(12).ToString()) - 1
            cboOccBuild.SelectedIndex = CInt(dt.Rows(0)(13).ToString()) - 1
            dtOccConstP.Value = CDate(dt.Rows(0)(14).ToString())
            dtOccConstA.Value = CDate(dt.Rows(0)(15).ToString())
            dtOccComP.Value = CDate(dt.Rows(0)(16).ToString())
            dtOccComA.Value = CDate(dt.Rows(0)(17).ToString())
            txtOccAreaP.Text = dt.Rows(0)(18).ToString()
            txtOccAreaA.Text = dt.Rows(0)(19).ToString()
            txtOccStorey.Text = dt.Rows(0)(20).ToString()
            txtOccCost.Text = FormatNumber(dt.Rows(0)(21).ToString(), 2)
        End If
    End Sub

    Private Sub btnFeeClose_Click(sender As System.Object, e As System.EventArgs) Handles btnFeeClose.Click
        slideFees.IsOpen = False
        panelSideBar.Enabled = True
        picFees.Image = My.Resources.Icon_Chart
        expFees.Expanded = False
        expFees.TitleStyle.ForeColor.Color = Color.White
        expFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelFees.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
    End Sub
#Region "Payment New Form"

    Private Sub txtPaymentPaid_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPaymentPaid.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtBuildConstFee.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub picPanelPayment_Click(sender As System.Object, e As System.EventArgs) Handles picPanelPayment.Click, picPayment.Click
        If expPayment.Expanded = True Then
            expPayment.Expanded = False
        Else
            expPayment.Expanded = True
            panelSideBar.Enabled = False
        End If
    End Sub

#End Region


    Private Sub expPayment_ExpandedChanged(sender As System.Object, e As DevComponents.DotNetBar.ExpandedChangeEventArgs) Handles expPayment.ExpandedChanged
        If expPayment.Expanded = True Then
            Call sideBarSelection(expPayment, picPanelPayment)
            Call writeAllText(panelNewPayments)
            Call generatePaymentID()
            Call generateORNumber()
            slidePayment.IsOpen = True
            loadBarangaysAssessment()
            loadSubdivisionsAssessment()
        End If
    End Sub

    Private Sub btnPaymentClose_Click(sender As System.Object, e As System.EventArgs) Handles btnPaymentClose.Click
        slidePayment.IsOpen = False
        picPayment.Image = My.Resources.Icon_Dollar
        expPayment.Expanded = False
        expPayment.TitleStyle.ForeColor.Color = Color.White
        expPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        picPanelPayment.Style.BackColor1.Color = Color.FromArgb(52, 69, 99)
        listPayments.Visible = False
        panelAddPayment.Visible = True
        Call clearAllText(panelNewPayments)
        btnPayments.Text = "Payment List"
        panelSideBar.Enabled = True
        CheckBox1.Visible = False
        PanelEx50.Enabled = False
        PanelEx50.Visible = False
        btnFilter1.Visible = False
        btnPrint1.Visible = False
    End Sub

    Private Sub btnSearchApplicant_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchApplicant.Click
        slidePayment.Enabled = False
        panelSearch.Visible = True
        searchmode = "Payment"
    End Sub

    Private Sub txtPaymentPaid_KeyUp(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtPaymentPaid.KeyUp
        Dim amount As Double
        If txtPaymentPaid.Text = "" Then
            amount = 0
        Else
            amount = CDbl(txtPaymentPaid.Text)
        End If
        txtPaymentChange.Text = FormatNumber(amount - CDbl(txtPaymentAssess.Text), 2)
    End Sub

    Private Sub txtPaymentPaid_Leave(sender As System.Object, e As System.EventArgs) Handles txtPaymentPaid.Leave
        If txtPaymentPaid.Text.Trim = "" Then
            txtPaymentPaid.Text = "0.00"
        Else
            txtPaymentPaid.Text = FormatNumber(CDbl(txtPaymentPaid.Text), 2)
        End If

    End Sub

    Sub loadPendingList(Optional ByVal otherfilter As String = "")
        listPayments.Items.Clear()
        Dim sql As String = "SELECT " & _
            "'-----' PaymentID, " & _
            "tblApp.Date PaymentDate, " & _
            "Lastname, " & _
            "FirstName, " & _
            "MiddleName, " & _
            "tblApp.ACN, " & _
            "Project, " & _
            "Lot, " & _
            "Block, " & _
            "Phase, " & _
            "CONCAT(SubdID,' - ',SubdName) SubdivisionCode, " & _
            "OtherInfo, " & _
            "Zone, " & _
            "CONCAT(BrgyID,' - ',BrgyName) Barangay, " & _
            "'-----' PaymentOR, " & _
            "'-----' ORDate, " & _
            "TotalAssess AS PaymentAssessment, " & _
            "'0.00' PaymentAmount, " & _
            "'0.00' PaymentChange " & _
            "FROM tblassessmentapplicant tblApp " & _
            "INNER JOIN tblassessmentsummary ON tblassessmentsummary.ACN = tblApp.ACN " & _
            "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
            "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID WHERE tblApp.Status = 'PENDING' " & otherfilter & " ORDER BY CAST(tblApp.ACN AS UNSIGNED)"
        Dim dtPending As DataTable = executeQuery(sql, "tblPending")
        If dtPending.Rows.Count > 0 Then
            For reccount As Integer = 0 To dtPending.Rows.Count - 1
                Dim listItem As ListViewItem = listPayments.Items.Add(dtPending.Rows(reccount)(0).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(1).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(2).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(3).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(4).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(5).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(6).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(7).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(8).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(9).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(10).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(11).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(12).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(13).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(14).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(15).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(16).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(17).ToString)
                listItem.SubItems.Add(dtPending.Rows(reccount)(18).ToString)
            Next
        End If
    End Sub

    Sub loadPaymentList(Optional ByVal otherfilter As String = "")
        listPayments.Items.Clear()
        Dim sq As String = "SELECT " & _
            "PaymentID, " & _
            "PaymentDate, " & _
            "Lastname, " & _
            "FirstName, " & _
            "MiddleName, " & _
            "tblPayments.ACN, " & _
            "Project, " & _
            "Lot, " & _
            "Block, " & _
            "Phase, " & _
            "CONCAT(SubdID,' - ',SubdName) SubdivisionCode, " & _
            "OtherInfo, " & _
            "Zone, " & _
            "CONCAT(BrgyID,' - ',BrgyName) Barangay, " & _
            "PaymentOR, " & _
            "ORDate, " & _
            "PaymentAssessment, " & _
            "PaymentAmount, " & _
            "(PaymentAmount - PaymentAssessment) PaymentChange " & _
            "FROM tblassessmentapplicant tblApp " & _
            "INNER JOIN tblPayments ON tblPayments.ACN = tblApp.ACN " & _
            "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
            "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID " & otherfilter & " ORDER BY CAST(PaymentID AS UNSIGNED)"
        Dim dt As DataTable = executeQuery(sq, "tblPayments")
        If dt.Rows.Count > 0 Then
            For reccount As Integer = 0 To dt.Rows.Count - 1
                Dim listItem As ListViewItem = listPayments.Items.Add(dt.Rows(reccount)(0).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(1).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(2).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(3).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(4).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(5).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(6).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(7).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(8).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(9).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(10).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(11).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(12).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(13).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(14).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(15).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(16).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(17).ToString)
                listItem.SubItems.Add(dt.Rows(reccount)(18).ToString)
            Next
        End If
    End Sub

    Private Sub btnPayments_Click(sender As System.Object, e As System.EventArgs) Handles btnPayments.Click
        If btnPayments.Text = "Payment List" Then
            itemType = "Paid"
            Call logtrail(userID, "Payment List", "Assessment", "***")
            listPayments.Visible = True
            panelAddPayment.Visible = False
            Call clearAllText(panelNewPayments)
            Call loadPaymentList()
            CheckBox1.Visible = True
            PanelEx50.Visible = True
            btnFilter1.Visible = True
            btnPrint1.Visible = True
            btnSavePayment.Visible = False
            btnPaymentClose.Visible = False
            Label184.Text = "Payment made from : "
            btnPending.Visible = False
            btnPayments.Visible = False
            Button8.Visible = True
        End If
    End Sub

    Private Sub btnSavePayment_Click(sender As System.Object, e As System.EventArgs) Handles btnSavePayment.Click
        If txtPaymentACN.Text = "" Then
            MessageBoxEx.Show("Please add applicant using the applicant search button.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtPaymentOR.Text.Trim = "" Then
            MessageBoxEx.Show("O.R. Number is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPaymentOR.Focus()
            Exit Sub
        End If
        If txtPaymentPaid.Text.Trim = "" Then
            MessageBoxEx.Show("Amount paid is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPaymentPaid.Focus()
            Exit Sub
        End If
        If CDbl(txtPaymentPaid.Text) < CDbl(txtPaymentAssess.Text) Then
            MessageBoxEx.Show("Amount paid is less than the total amount to be settled.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPaymentPaid.Focus()
            Exit Sub
        End If
        Dim q As String = "INSERT INTO tblpayments(PaymentID, PaymentDate, ACN, PaymentOR, ORDate, PaymentAmount, PaymentAssessment) " & _
                "VALUES ('" & initializeQueryEntry(txtPID.Text.Trim, False) & "','" & initializeQueryEntry(dtPaymentDate.Value.ToShortDateString, False) & _
                "','" & initializeQueryEntry(txtPaymentACN.Text.Trim, False) & "','" & initializeQueryEntry(txtPaymentOR.Text.Trim, False) & _
                "','" & initializeQueryEntry(dtPaymentOR.Value.ToShortDateString, False) & "','" & initializeQueryEntry(CDbl(txtPaymentPaid.Text.Trim), False) & _
                "','" & initializeQueryEntry(CDbl(txtPaymentAssess.Text.Trim), False) & "')"
        executeQuery(q, "insertPayment")
        q = "UPDATE tblassessmentapplicant SET Status = 'Paid' WHERE ACN = '" & initializeQueryEntry(txtPaymentACN.Text.Trim, False) & "'"
        executeQuery(q, "updateAssessment")
        MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Call logtrail(userID, "Add", "Payment", txtPID.Text.Trim)
        Call clearAllText(panelNewPayments)
        Call generatePaymentID()
        Call generateORNumber()
        loadBarangaysAssessment()
        loadSubdivisionsAssessment()
    End Sub

    Private Sub txtAssessPermitSub_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAssessPermitSub.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOccCost.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If
    End Sub

    Private Sub txtAssessPermitPre_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtAssessPermitPre.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOccCost.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If
    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        btnSummary.PerformClick()
        Dim frmRep As New formReport
        Dim rpt As New rptSummary
        Dim dsR As New dsReportSummary

        Dim daR As New Odbc.OdbcDataAdapter( _
            "SELECT " & _
            "	SUM(BuildConst) BuildConst, " & _
            "	SUM(ElecInst) ElecInst, " & _
            "	SUM(MechIns) MechIns, " & _
            "	SUM(PlumbIns) PlumbIns, " & _
            "	SUM(ElectroIns) ElectroIns, " & _
            "	SUM(BuildAcc) BuildAcc, " & _
            "	SUM(OtherAcc) OtherAcc, " & _
            "	SUM(BuildOcc) BuildOcc, " & _
            "	SUM(BuildInsp) BuildInsp, " & _
            "	SUM(CertFee) CertFee, " & _
            "	SUM(Fines) Fines, " & _
            "	SUM(TOTALAssess) TOTALAssess, " & _
            "	SUM(LOCAL) LOCAL, " & _
            "	SUM(Natl) Natl, " & _
            "	SUM(OBO) OBO " & _
            "FROM " & _
            "	tblassessmentsummary tblSum " & _
            "INNER JOIN tblassessmentapplicant tblApp ON tblSum.ACN = tblApp.acn " & _
            "WHERE " & _
            "	(STR_TO_DATE(DATE, '%m/%d/%Y') BETWEEN STR_TO_DATE('" & dtFromSummary.Value.ToShortDateString & "', '%m/%d/%Y') " & _
            "AND STR_TO_DATE('" & dtToSummary.Value.ToShortDateString & "', '%m/%d/%Y')) AND tblApp.Status = 'Paid'", sqlCon)
        daR.Fill(dsR, "dtReportSummary")
        rpt.SetDataSource(dsR.Tables(0))

        remark_A = New CrystalDecisions.Shared.ParameterDiscreteValue
        remark_B = New CrystalDecisions.Shared.ParameterValues

        remark_A.Value = Format(dtFromSummary.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateFrom").ApplyCurrentValues(remark_B)


        remark_A.Value = Format(dtToSummary.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateTo").ApplyCurrentValues(remark_B)

        frmRep.CrystalReportViewer1.ReportSource = rpt
        Call logtrail(userID, "Print Summary", "Payment Summary", "***")
        frmRep.ShowDialog()
    End Sub

    Sub voidOldAssessments(ByVal intDays As String)
        Dim q As String = "SELECT ACN, CONCAT(LastName,', ',FirstName), Date FROM tblassessmentapplicant WHERE STR_TO_DATE(Date, '%m/%d/%Y')  <= SUBDATE(CURDATE(),INTERVAL " & intDays & " DAY) AND Status = 'Pending'"
        Dim dt As DataTable = executeQuery(q, "tblGetVoid")
        If dt.Rows.Count > 0 Then
            Dim qr As String = "UPDATE tblassessmentapplicant SET Status = 'Void' WHERE STR_TO_DATE(Date, '%m/%d/%Y')  <= SUBDATE(CURDATE(),INTERVAL " & intDays & " DAY) AND Status = 'Pending'"
            executeQuery(qr, "voidItems")
            Dim listVoid As String = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                listVoid &= "ACN " & dt.Rows(i)(0).ToString & " - " & dt.Rows(i)(1).ToString & " (" & dt.Rows(i)(2).ToString & ")" & vbNewLine
            Next
            MessageBoxEx.Show("The following Assessments has been voided due to their unsettled" & vbNewLine & _
                              "payments for the given period of time (" & intDays & " days)." & vbNewLine & _
                              vbNewLine & listVoid, My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            PanelEx50.Enabled = True
        Else
            dtPayList1.Value = Date.Today
            dtPayList2.Value = Date.Today.AddDays(1)
            PanelEx50.Enabled = False
            If itemType = "Paid" Then
                Call loadPaymentList()
            Else
                Call loadPendingList()
            End If

        End If
    End Sub

    Sub getTrail(Optional ByVal filter As String = "")
        listTrail.Items.Clear()
        Dim q As String = "SELECT tblT.Date, tblT.Assessor, CONCAT(Username,' - ',FirstName,' ',LastName) Name, tblT.Transaction " & _
                        "FROM tblTrail tblT " & _
                        "INNER JOIN tblUsers tblU " & _
                        "ON tblT.Assessor = tblU.UserID " & filter & " " & _
                        "ORDER BY STR_TO_DATE(tblT.Date,'%m/%d/%Y %H:%i:%s') "
        Dim dt As DataTable = executeQuery(q, "getTrail")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim listI As ListViewItem
                listI = listTrail.Items.Add(dt.Rows(i)(0).ToString)
                listI.SubItems.Add(dt.Rows(i)(1).ToString)
                listI.SubItems.Add(dt.Rows(i)(2).ToString)
                listI.SubItems.Add(dt.Rows(i)(3).ToString)
            Next
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            PanelEx53.Enabled = True
        Else
            PanelEx53.Enabled = False
            Call getTrail()
        End If
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If CheckBox2.Checked Then
            If dtTrail1.Value > dtTrail2.Value Then
                MessageBoxEx.Show("Date from is greater than date to.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Call getTrail("WHERE STR_TO_DATE(tblT.Date,'%m/%d/%Y %H:%i:%s') BETWEEN STR_TO_DATE('" & initializeQueryEntry(dtTrail1.Value.ToShortDateString, True) & "','%m/%d/%Y') AND STR_TO_DATE('" & initializeQueryEntry(dtTrail2.Value.ToShortDateString, True) & "','%m/%d/%Y')")
        Else
            Call getTrail()
        End If

    End Sub

    Private Sub ButtonX4_Click(sender As System.Object, e As System.EventArgs) Handles ButtonX4.Click
        slideTrail.IsOpen = True
        Call getTrail()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        PanelEx53.Enabled = False
        slideTrail.IsOpen = False
        CheckBox2.Checked = False
    End Sub

    Private Sub btnFilter1_Click(sender As System.Object, e As System.EventArgs) Handles btnFilter1.Click
        If CheckBox1.Checked Then
            If dtPayList1.Value > dtPayList2.Value Then
                MessageBoxEx.Show("Date from is greater than date to.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If itemType = "Paid" Then
                Call loadPaymentList(" WHERE STR_TO_DATE(PaymentDate,'%m/%d/%Y') BETWEEN STR_TO_DATE('" & initializeQueryEntry(dtPayList1.Value.ToShortDateString, True) & "','%m/%d/%Y') AND STR_TO_DATE('" & initializeQueryEntry(dtPayList2.Value.ToShortDateString, True) & "','%m/%d/%Y') ")
            Else
                Call loadPendingList(" AND (STR_TO_DATE(tblApp.Date,'%m/%d/%Y') BETWEEN STR_TO_DATE('" & initializeQueryEntry(dtPayList1.Value.ToShortDateString, True) & "','%m/%d/%Y') AND STR_TO_DATE('" & initializeQueryEntry(dtPayList2.Value.ToShortDateString, True) & "','%m/%d/%Y')) ")
            End If

        Else
            If itemType = "Paid" Then
                Call loadPaymentList()
            Else
                Call loadPendingList()
            End If
        End If
    End Sub

    'Sub loadPaid(ByVal acn As String)
    '    Dim q As String = "SELECT tblSum.ACN, Date, tblApp.LastName, tblApp.FirstName, " & _
    '   "tblApp.MiddleName, Project, Lot, Block, Phase, " & _
    '   "SubdivisionCode, OtherInfo, Zone, BarangayCode, Remarks, PermitPre, PermitSub, " & _
    '   "PermitDate, Encoder, Additional, " & _
    '   "BuildConst, ElecInst, MechIns, PlumbIns, ElectroIns, BuildAcc, " & _
    '   "OtherAcc, BuildOcc, BuildInsp, CertFee, Fines, TOTALAssess, " & _
    '   "Local, Natl, OBO, CONCAT(tblUsers.FirstName,' ',tblUsers.LastName), tblApp.Status " & _
    '   "FROM tblassessmentapplicant tblApp " & _
    '   "INNER JOIN tblassessmentsummary tblSum " & _
    '   "ON tblApp.ACn = tblSum.ACN " & _
    '   "INNER JOIN tblUsers " & _
    '   "ON tblUsers.UserID = tblApp.Encoder " & _
    '   "WHERE tblApp.ACN = '" & initializeQueryEntry(acn, False) & "'"
    '    Dim dtSearchItem As DataTable = executeQuery(q, "tblSearchResultPaid")

    'End Sub

    Sub loadPaymentItem(ByVal acn As String)
        Call loadBarangaysAssessment()
        Call loadSubdivisionsAssessment()
        Dim q As String = "SELECT tblSum.ACN, Date, tblApp.LastName, tblApp.FirstName, " & _
        "tblApp.MiddleName, Project, Lot, Block, Phase, " & _
        "SubdivisionCode, OtherInfo, Zone, BarangayCode, Remarks, PermitPre, PermitSub, " & _
        "PermitDate, Encoder, Additional, " & _
        "BuildConst, ElecInst, MechIns, PlumbIns, ElectroIns, BuildAcc, " & _
        "OtherAcc, BuildOcc, BuildInsp, CertFee, Fines, TOTALAssess, " & _
        "Local, Natl, OBO, CONCAT(tblUsers.FirstName,' ',tblUsers.LastName), tblApp.Status " & _
        "FROM tblassessmentapplicant tblApp " & _
        "INNER JOIN tblassessmentsummary tblSum " & _
        "ON tblApp.ACn = tblSum.ACN " & _
        "INNER JOIN tblUsers " & _
        "ON tblUsers.UserID = tblApp.Encoder " & _
        "WHERE tblApp.ACN = '" & initializeQueryEntry(acn, False) & "'"
        Dim dtSearchItem As DataTable = executeQuery(q, "tblSearchResult")
        If dtSearchItem.Rows.Count > 0 Then
            Dim subDiv As String
            If dtSearchItem.Rows(0)(9).ToString = "" Then
                subDiv = "0"
            Else
                subDiv = dtSearchItem.Rows(0)(9).ToString
            End If

            txtPaymentACN.Text = dtSearchItem.Rows(0)(0).ToString
            txtPaymentProject.Text = dtSearchItem.Rows(0)(5).ToString
            txtPaymentL.Text = dtSearchItem.Rows(0)(2).ToString
            txtPaymentF.Text = dtSearchItem.Rows(0)(3).ToString
            txtPaymentM.Text = dtSearchItem.Rows(0)(4).ToString
            txtPaymentLot.Text = dtSearchItem.Rows(0)(6).ToString
            txtPaymentBlock.Text = dtSearchItem.Rows(0)(7).ToString
            txtPaymentPhase.Text = dtSearchItem.Rows(0)(8).ToString
            cboPaymentSub.SelectedIndex = CInt(subDiv) - 1
            txtPaymentStreet.Text = dtSearchItem.Rows(0)(10).ToString
            txtPaymentZone.Text = dtSearchItem.Rows(0)(11).ToString
            cboPaymentBgy.SelectedIndex = CInt(dtSearchItem.Rows(0)(12).ToString) - 1
            txtPaymentAssess.Text = FormatNumber(dtSearchItem.Rows(0)(30).ToString, 2)

            txtFee1.Text = FormatNumber(dtSearchItem.Rows(0)(19).ToString, 2)
            txtFee2.Text = FormatNumber(dtSearchItem.Rows(0)(20).ToString, 2)
            txtFee3.Text = FormatNumber(dtSearchItem.Rows(0)(21).ToString, 2)
            txtFee4.Text = FormatNumber(dtSearchItem.Rows(0)(22).ToString, 2)
            txtFee5.Text = FormatNumber(dtSearchItem.Rows(0)(23).ToString, 2)
            txtFee6.Text = FormatNumber(dtSearchItem.Rows(0)(24).ToString, 2)
            txtFee7.Text = FormatNumber(dtSearchItem.Rows(0)(25).ToString, 2)
            txtFee8.Text = FormatNumber(dtSearchItem.Rows(0)(26).ToString, 2)
            txtFee9.Text = FormatNumber(dtSearchItem.Rows(0)(27).ToString, 2)
            txtFee10.Text = FormatNumber(dtSearchItem.Rows(0)(28).ToString, 2)
            txtFee11.Text = FormatNumber(dtSearchItem.Rows(0)(29).ToString, 2)


            txtPaymentPaid.Text = FormatNumber(CDbl(listPayments.FocusedItem.SubItems(17).Text), 2)
            txtPaymentChange.Text = FormatNumber(CDbl(listPayments.FocusedItem.SubItems(18).Text), 2)
            If itemType = "Paid" Then
                txtPID.Text = listPayments.FocusedItem.SubItems(5).Text
                txtPaymentOR.Text = listPayments.FocusedItem.SubItems(14).Text
                panelPaid.Visible = True
                panelPending.Visible = False
                Label162.Visible = True
                dtPaymentDate.Visible = True
                PanelEx47.Text = "Payment Breakdown"
            Else
                Label162.Visible = False
                dtPaymentDate.Visible = False
                txtPID.Text = ""
                txtPaymentOR.Text = ""
                panelPaid.Visible = False
                panelPending.Visible = True
                PanelEx47.Text = "Assessment Breakdown"
            End If
            Button6.Visible = True
            listPayments.Visible = False

            btnSearchApplicant.Visible = False
            lblIndicate.Visible = False
            Call readAllText(panelNewPayments)

            CheckBox1.Visible = False
            PanelEx50.Visible = False
            btnFilter1.Visible = False
            btnPrint1.Visible = False
            panelAddPayment.Visible = True

            btnPayments.Visible = False
            btnSavePayment.Visible = False
            btnPaymentClose.Visible = False
            btnPending.Visible = False
        End If
    End Sub

    Private Sub listPayments_DoubleClick(sender As System.Object, e As System.EventArgs) Handles listPayments.DoubleClick
        Call loadPaymentItem(listPayments.FocusedItem.SubItems(5).Text)
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        txtPaymentPaid.Text = ""
        txtPaymentChange.Text = ""
        txtPID.Text = ""
        txtPaymentOR.Text = ""
        panelPaid.Visible = False
        panelPending.Visible = False
        Button6.Visible = False
        listPayments.Visible = True

        btnSearchApplicant.Visible = True
        lblIndicate.Visible = True
        panelNewPayments.Enabled = True

        CheckBox1.Visible = True
        PanelEx50.Visible = True
        btnFilter1.Visible = True
        btnPrint1.Visible = True
        panelAddPayment.Visible = False
        Call writeAllText(panelNewPayments)
        Button8.Visible = True

        Label162.Visible = True
        dtPaymentDate.Visible = True
        PanelEx47.Text = "Payment Breakdown"
        'btnPayments.Visible = True
        'btnSavePayment.Visible = True
        'btnPaymentClose.Visible = True
    End Sub

    Private Sub btnPrint1_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint1.Click
        If CheckBox1.Checked Then
            If dtPayList1.Value > dtPayList2.Value Then
                MessageBoxEx.Show("Date from is greater than date to.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If itemType = "Paid" Then
                Call printPayments(" WHERE STR_TO_DATE(PaymentDate,'%m/%d/%Y') BETWEEN STR_TO_DATE('" & initializeQueryEntry(dtPayList1.Value.ToShortDateString, True) & "','%m/%d/%Y') AND STR_TO_DATE('" & initializeQueryEntry(dtPayList2.Value.ToShortDateString, True) & "','%m/%d/%Y') ")
            Else
                Call printPending(" AND (STR_TO_DATE(tblApp.Date,'%m/%d/%Y') BETWEEN STR_TO_DATE('" & initializeQueryEntry(dtPayList1.Value.ToShortDateString, True) & "','%m/%d/%Y') AND STR_TO_DATE('" & initializeQueryEntry(dtPayList2.Value.ToShortDateString, True) & "','%m/%d/%Y')) ")
            End If

        Else
            If itemType = "Paid" Then
                Call printPayments()
            Else
                Call printPending()
            End If

        End If
    End Sub

    Sub printPending(Optional ByVal otherfilter As String = "")

        Dim frmRep As New formReport
        Dim rpt As New rptPayments
        Dim dsRPen As New dsPaymentReport

        Dim daRPen As New Odbc.OdbcDataAdapter("SELECT " & _
            "'-----' PaymentID, " & _
            "tblApp.Date PaymentDate, " & _
            "Lastname, " & _
            "FirstName, " & _
            "MiddleName, " & _
            "tblApp.ACN, " & _
            "Project, " & _
            "Lot, " & _
            "Block, " & _
            "Phase, " & _
            "CONCAT(SubdID,' - ',SubdName) SubdivisionCode, " & _
            "OtherInfo, " & _
            "Zone, " & _
            "CONCAT(BrgyID,' - ',BrgyName) Barangay, " & _
            "'-----' PaymentOR, " & _
            "'-----' ORDate, " & _
            "TotalAssess AS PaymentAssessment, " & _
            "'0.00' PaymentAmount, " & _
            "'0.00' PaymentChange " & _
            "FROM tblassessmentapplicant tblApp " & _
            "INNER JOIN tblassessmentsummary ON tblassessmentsummary.ACN = tblApp.ACN " & _
            "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
            "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID WHERE tblApp.Status = 'PENDING' " & otherfilter & " ORDER BY CAST(tblApp.ACN AS UNSIGNED)", sqlCon)
        daRPen.Fill(dsRPen, "dtPayment")
        rpt.SetDataSource(dsRPen.Tables(0))

        remark_A = New CrystalDecisions.Shared.ParameterDiscreteValue
        remark_B = New CrystalDecisions.Shared.ParameterValues

        remark_A.Value = itemType
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("ReportType").ApplyCurrentValues(remark_B)

        remark_A.Value = Format(dtPayList1.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateFrom").ApplyCurrentValues(remark_B)


        remark_A.Value = Format(dtPayList2.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateTo").ApplyCurrentValues(remark_B)

        frmRep.CrystalReportViewer1.ReportSource = rpt
        Call logtrail(userID, "Print Pending List", "Pending List", "***")
        frmRep.ShowDialog()
    End Sub

    Sub printPayments(Optional ByVal otherfilter As String = "")

        Dim frmRep As New formReport
        Dim rpt As New rptPayments
        Dim dsR As New dsPaymentReport

        Dim daR As New Odbc.OdbcDataAdapter("SELECT " & _
            "PaymentID, " & _
            "PaymentDate, " & _
            "Lastname, " & _
            "FirstName, " & _
            "MiddleName, " & _
            "tblPayments.ACN ACNo, " & _
            "Project, " & _
            "Lot, " & _
            "Block, " & _
            "Phase, " & _
            "CONCAT(SubdID,' - ',SubdName) SubdivisionCode, " & _
            "OtherInfo, " & _
            "Zone, " & _
            "CONCAT(BrgyID,' - ',BrgyName) Barangay, " & _
            "PaymentOR, " & _
            "ORDate, " & _
            "PaymentAssessment, " & _
            "PaymentAmount, " & _
            "(PaymentAmount - PaymentAssessment) PaymentChange, " & _
            "CONCAT(PermitPre,'-',PermitSub) PermitNo " & _
            "FROM tblassessmentapplicant tblApp " & _
            "INNER JOIN tblPayments ON tblPayments.ACN = tblApp.ACN " & _
            "LEFT JOIN tblsubdivisions tblBrgy ON SubDivisionCode = SubdID " & _
            "LEFT JOIN tblbarangays tblSubd ON BarangayCode = brgyID " & otherfilter & " ORDER BY CAST(PaymentID AS UNSIGNED)", sqlCon)
        daR.Fill(dsR, "dtPayment")
        rpt.SetDataSource(dsR.Tables(0))

        remark_A = New CrystalDecisions.Shared.ParameterDiscreteValue
        remark_B = New CrystalDecisions.Shared.ParameterValues

        remark_A.Value = "Payment"
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("ReportType").ApplyCurrentValues(remark_B)

        remark_A.Value = Format(dtPayList1.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateFrom").ApplyCurrentValues(remark_B)


        remark_A.Value = Format(dtPayList2.Value, "MMM dd, yyyy")
        remark_B.Add(remark_A)
        rpt.DataDefinition.ParameterFields("dateTo").ApplyCurrentValues(remark_B)

        frmRep.CrystalReportViewer1.ReportSource = rpt
        Call logtrail(userID, "Print Payment List", "Payment List", "***")
        frmRep.ShowDialog()
    End Sub

    Private Sub txtPaymentOR_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPaymentOR.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOccStorey_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtOccStorey.TextChanged

    End Sub

    Private Sub txtOccStorey_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOccStorey.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOccAreaP_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOccAreaP.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOccAreaP.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtOccAreaA_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtOccAreaA.KeyPress
        Dim FullStop As Char
        FullStop = "."

        ' if the '.' key was pressed see if there already is a '.' in the string
        ' if so, dont handle the keypress
        If e.KeyChar = FullStop And txtOccAreaA.Text.IndexOf(FullStop) <> -1 Then
            e.Handled = True
            Return
        End If

        ' If the key aint a digit
        If Not Char.IsDigit(e.KeyChar) Then
            ' verify whether special keys were pressed
            ' (i.e. all allowed non digit keys - in this example
            ' only space and the '.' are validated)
            If (e.KeyChar <> FullStop) And
               (e.KeyChar <> Convert.ToChar(Keys.Back)) Then
                ' if its a non-allowed key, dont handle the keypress
                e.Handled = True
                Return
            End If
        End If
    End Sub

    Private Sub txtOccAreaP_Leave(sender As Object, e As System.EventArgs) Handles txtOccAreaP.Leave
        If txtOccAreaP.Text.Trim = "" Then
            txtOccAreaP.Text = FormatNumber(0, 2)
        Else
            txtOccAreaP.Text = FormatNumber(txtOccAreaP.Text, 2)
        End If
    End Sub

    Private Sub txtOccAreaA_Leave(sender As Object, e As System.EventArgs) Handles txtOccAreaA.Leave
        If txtOccAreaA.Text.Trim = "" Then
            txtOccAreaA.Text = FormatNumber(0, 2)
        Else
            txtOccAreaA.Text = FormatNumber(txtOccAreaA.Text, 2)
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        imageUpload = ""
        picSystemProfile.Image = My.Resources.DefaultUser
    End Sub

    Private Sub btnAssessmentEdit_Click(sender As System.Object, e As System.EventArgs) Handles btnAssessmentEdit.Click
        If txtAssessStatus.Text = "Paid" Then
            MessageBoxEx.Show("You cannot modify a paid assessment.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtAssessStatus.Text = "Void" Then
            MessageBoxEx.Show("You cannot modify an already voided assessment.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If btnAssessmentEdit.Text = "&Edit" Then
            Call writeAllText(panelAssessment)
            Call writeAllText(panelAssessSum)
            txtAssessRemarks.ReadOnly = True
            btnAssessmentEdit.Text = "&Update"
            btnAssessmentClose.Text = "&Cancel"
            btnAssessmentFind.Enabled = False
            btnAssessmentNew.Enabled = False
        Else
            If txtAssessLast.Text.Trim = "" Then
                MessageBoxEx.Show("Family name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessLast.Focus()
                Exit Sub
            End If
            If txtAssessFirst.Text.Trim = "" Then
                MessageBoxEx.Show("First name is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessFirst.Focus()
                Exit Sub
            End If
            If txtAssessProject.Text.Trim = "" Then
                MessageBoxEx.Show("Project is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessProject.Focus()
                Exit Sub
            End If
            If cbo_Barangay_Assess.SelectedIndex = -1 Then
                MessageBoxEx.Show("Barangay is required.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cbo_Barangay_Assess.Focus()
                Exit Sub
            End If
            If checkExistProject(txtAssessProject.Text.Trim, txtACN.Text) Then
                MessageBoxEx.Show("Project name already exists.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtAssessProject.Focus()
                Exit Sub
            End If
            Call updateAssessmentApplicant()
            Call updateAssessmentSummary()
            MessageBoxEx.Show("Record successfully saved.", My.Resources.PopupTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call logtrail(userID, "Edit", "Assessment", txtACN.Text.Trim)
            Call readAllText(panelAssessment)
            Call readAllText(panelAssessSum)
            Call resetZeroAllText(panelAssessSum)
            Call clearAllText(panelAssessment)
            Call loadBarangaysAssessment()
            Call loadSubdivisionsAssessment()
            btnAssessmentFind.Enabled = True
            btnAssessmentNew.Enabled = True
            btnAssessmentEdit.Text = "&Edit"
            btnAssessmentClose.Text = "&Close"
            btnAssessmentEdit.Enabled = False
        End If
    End Sub

    Private Sub btnPending_Click(sender As System.Object, e As System.EventArgs) Handles btnPending.Click
        If btnPending.Text = "Pending List" Then
            itemType = "Pending"
            Call logtrail(userID, "Pending List", "Assessment", "***")
            listPayments.Visible = True
            panelAddPayment.Visible = False
            Call clearAllText(panelNewPayments)
            Call loadPendingList()
            CheckBox1.Visible = True
            PanelEx50.Visible = True
            btnFilter1.Visible = True
            btnPrint1.Visible = True
            btnSavePayment.Visible = False
            btnPaymentClose.Visible = False
            Label184.Text = "Pending Items from : "
            btnPayments.Visible = False
            btnPending.Visible = False
            Button8.Visible = True
        End If
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        itemType = ""
        listPayments.Visible = False
        panelAddPayment.Visible = True
        Call clearAllText(panelNewPayments)
        Call generatePaymentID()
        Call generateORNumber()
        loadBarangaysAssessment()
        loadSubdivisionsAssessment()
        CheckBox1.Visible = False
        PanelEx50.Visible = False
        btnFilter1.Visible = False
        btnPrint1.Visible = False
        btnSavePayment.Visible = True
        btnPaymentClose.Visible = True
        Button8.Visible = False
        btnPending.Visible = True
        btnPayments.Visible = True
    End Sub
End Class



