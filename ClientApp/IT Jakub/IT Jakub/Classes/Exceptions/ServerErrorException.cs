﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IT_Jakub.Classes.Exceptions {
    
    class ServerErrorException : MyException {

        public ServerErrorException(Exception e) : base("Vyskytl se problém při kontaktování serveru, ověřte své připojení k internetu a zkuste to znovu."){
            invoker = e;
        }
    }
}
