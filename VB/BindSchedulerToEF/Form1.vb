Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.Objects

Namespace BindSchedulerToEF
    Partial Public Class Form1
        Inherits Form

        Private entities As CarsDBEntities

        Public Sub New()
            InitializeComponent()

            SubscribeToEvents()
            SetupMappings()

            entities = New CarsDBEntities()
            schedulerStorage1.Appointments.DataSource = entities.CarSchedulings
            schedulerStorage1.Resources.DataSource = entities.Cars
        End Sub

        Private Sub SubscribeToEvents()
            AddHandler Me.schedulerStorage1.AppointmentsInserted, AddressOf Me.schedulerStorage1_AppointmentsModified
            AddHandler Me.schedulerStorage1.AppointmentsChanged, AddressOf Me.schedulerStorage1_AppointmentsModified
            AddHandler Me.schedulerStorage1.AppointmentsDeleted, AddressOf Me.schedulerStorage1_AppointmentsModified
        End Sub
        Private Sub SetupMappings()
            ' appointment mappings
            Me.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay"
            Me.schedulerStorage1.Appointments.Mappings.AppointmentId = "ID"
            Me.schedulerStorage1.Appointments.Mappings.Description = "Description"
            Me.schedulerStorage1.Appointments.Mappings.End = "EndTime"
            Me.schedulerStorage1.Appointments.Mappings.Label = "Label"
            Me.schedulerStorage1.Appointments.Mappings.Location = "Location"
            Me.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo"
            Me.schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo"
            Me.schedulerStorage1.Appointments.Mappings.ResourceId = "CarId"
            Me.schedulerStorage1.Appointments.Mappings.Start = "StartTime"
            Me.schedulerStorage1.Appointments.Mappings.Status = "Status"
            Me.schedulerStorage1.Appointments.Mappings.Subject = "Subject"
            Me.schedulerStorage1.Appointments.Mappings.Type = "EventType"

            ' resource mappings
            Me.schedulerStorage1.Resources.Mappings.Caption = "Model"
            Me.schedulerStorage1.Resources.Mappings.Id = "ID"
            Me.schedulerStorage1.Resources.Mappings.Image = "Picture"
        End Sub

        Private Sub schedulerStorage1_AppointmentsModified(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.PersistentObjectsEventArgs)
            entities.SaveChanges()
            entities.AcceptAllChanges()
        End Sub
    End Class
End Namespace