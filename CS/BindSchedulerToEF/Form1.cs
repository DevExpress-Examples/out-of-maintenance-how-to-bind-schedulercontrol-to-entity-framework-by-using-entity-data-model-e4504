using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;

namespace BindSchedulerToEF {
    public partial class Form1 : Form {

        CarsDBEntities entities;

        public Form1() {
            InitializeComponent();

            SubscribeToEvents();
            SetupMappings();

            entities = new CarsDBEntities();
            schedulerStorage1.Appointments.DataSource = entities.CarSchedulings;
            schedulerStorage1.Resources.DataSource = entities.Cars;
        }

        void SubscribeToEvents() {
            this.schedulerStorage1.AppointmentsInserted += this.schedulerStorage1_AppointmentsModified;
            this.schedulerStorage1.AppointmentsChanged += this.schedulerStorage1_AppointmentsModified;
            this.schedulerStorage1.AppointmentsDeleted += this.schedulerStorage1_AppointmentsModified;
        }
        void SetupMappings() {
            // appointment mappings
            this.schedulerStorage1.Appointments.Mappings.AllDay = "AllDay";
            this.schedulerStorage1.Appointments.Mappings.AppointmentId = "ID";
            this.schedulerStorage1.Appointments.Mappings.Description = "Description";
            this.schedulerStorage1.Appointments.Mappings.End = "EndTime";
            this.schedulerStorage1.Appointments.Mappings.Label = "Label";
            this.schedulerStorage1.Appointments.Mappings.Location = "Location";
            this.schedulerStorage1.Appointments.Mappings.RecurrenceInfo = "RecurrenceInfo";
            this.schedulerStorage1.Appointments.Mappings.ReminderInfo = "ReminderInfo";
            this.schedulerStorage1.Appointments.Mappings.ResourceId = "CarId";
            this.schedulerStorage1.Appointments.Mappings.Start = "StartTime";
            this.schedulerStorage1.Appointments.Mappings.Status = "Status";
            this.schedulerStorage1.Appointments.Mappings.Subject = "Subject";
            this.schedulerStorage1.Appointments.Mappings.Type = "EventType";

            // resource mappings
            this.schedulerStorage1.Resources.Mappings.Caption = "Model";
            this.schedulerStorage1.Resources.Mappings.Id = "ID";
            this.schedulerStorage1.Resources.Mappings.Image = "Picture";
        }

        private void schedulerStorage1_AppointmentsModified(object sender, DevExpress.XtraScheduler.PersistentObjectsEventArgs e) {
            entities.SaveChanges();
            entities.AcceptAllChanges();
        }
    }
}