using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class Node
    {
        public Node next = null;
        public Node prev = null;
        private String firstName;
        private String lastName;
       // private Date birthDate;
       // private Address homeAddress;
      //  private Address schoolAddress;
      //  private Course[] coursesTaken;
      //  private String[] grades;


        public Node() { }

        public bool setFirstName(String s)
        {
            if (s != null && s.Length > 0)
            {
                firstName = s;
                return true;
            }
            return false;
        }

        public String getFirstName()
        {
            if (firstName != null)
                return firstName;

            return "firstName not initialized";
        }

    public bool setLastName(String s)
    {
        if (s != null && s.Length > 0)
        {
            lastName = s;
            return true;
        }
        return false;
    }

    public String getLastName()
    {
        if (lastName != null)
            return lastName;

        return "LastName not initialized";
    }
}
}
