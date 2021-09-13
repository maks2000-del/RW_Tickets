namespace Tickets.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Cryptography;
    using System.Text;

    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }
        #region db_fields
        public int id { get; set; }

        [StringLength(30)]
        public string firstName { get; set; }

        [StringLength(30)]
        public string secondName { get; set; }

        [StringLength(30)]
        public string patronymic { get; set; }

        [StringLength(50)]
        public string mail { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        [StringLength(20)]
        public string telNumber { get; set; }

        [StringLength(20)]
        public string date_of_birth { get; set; }

        [StringLength(20)]
        public string passport_id { get; set; }

        [StringLength(6)]
        public string sex { get; set; }

        [StringLength(5)]
        public string privilege { get; set; }

        #endregion db_fileds

        //preregistrathion constructor
        public User(string firstName, string mail, string password)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.mail = mail ?? throw new ArgumentNullException(nameof(mail));
            this.password = getHash(password) ?? throw new ArgumentNullException(nameof(password));
            this.privilege = "user";
        }

        //registrathion constructor
        public User(string firstName, string secondName, string patronymic, string mail, string telNumber, string date_of_birth, string passport_id, string sex)
        {
            this.firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            this.secondName = secondName ?? throw new ArgumentNullException(nameof(secondName));
            this.patronymic = patronymic ?? throw new ArgumentNullException(nameof(patronymic));
            this.mail = mail ?? throw new ArgumentNullException(nameof(mail));
            this.telNumber = telNumber ?? throw new ArgumentNullException(nameof(telNumber));
            this.date_of_birth = date_of_birth ?? throw new ArgumentNullException(nameof(date_of_birth));
            this.passport_id = passport_id ?? throw new ArgumentNullException(nameof(passport_id));
            this.sex = sex ?? throw new ArgumentNullException(nameof(sex));

            this.privilege = "user";
        }

        public static string getHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public virtual ICollection<Ticket> Tickets { get; set; }

        
    }
}
