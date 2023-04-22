﻿using System.Drawing.Printing;

namespace fullstackCsharp.Models
{
    public class Staff
    {
        
        private string manv;
        private string namenv;
        private string gender;
        private string phone;
        private string address;
        private string user;
        private string password;
        private int rank;
        private string status;
        private string id_pb;
        private string id_rank;

        public Staff()
        {
        }

        

        public Staff(string manv, string namenv, string phone, string gender, string address, string user, string password, int rank, string status, string id_pb, string id_rank)
        {
            this.manv = manv;
            this.namenv = namenv;
            this.phone = phone;
            this.gender = gender;
            this.address = address;
            this.user = user;
            this.password = password;
            this.rank = rank;
            this.status = status;
            this.id_pb = id_pb;
            this.id_rank = id_rank;
        }

        public string Manv { get => manv; set => manv = value; }
        public string Namenv { get => namenv; set => namenv = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Address { get => address; set => address = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public int Rank { get => Rank; set => Rank = value; }
        public string Status { get => status; set => status = value; }
        public string Id_pb { get => id_pb; set => id_pb = value; }
        public string Id_rank { get => id_rank; set => id_rank = value; }



        //public Staff(int)
    }
    
}
