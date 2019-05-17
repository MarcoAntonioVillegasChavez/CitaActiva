using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitaActiva.Util
{
    public class Constants
    {
        public const string AppointmentUrl = "https://qis.quiter.com/qis/api/appointments/v1/appointments";
        public const string WorkshopUrl = "https://qis.quiter.com/qis/api/appointments/v1/workshops";
        public const string TokenUrl = "https://qis.quiter.com/qis/oauth/token?grant_type=authorization_code&client_id=autocom&client_secret=b8ff7793e0c12960352a7d90211aaf7862e979eb&code=f2985720ce6b168e953df8318c496c752882f22f";
        public const string ReceptionistUrl = "https://qis.quiter.com/qis/api/appointments/v1/receptionists?active=true&workshopId=";
        public const string ReceptionistUrlSingle = "https://qis.quiter.com/qis/api/appointments/v1/receptionists/";
        public const string ScheduleUrl = "https://qis.quiter.com/qis/api/appointments/v1/schedules/";
    }
}
