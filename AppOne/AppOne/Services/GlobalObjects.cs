using AppOne.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace AppOne.Services
{
    internal class GlobalObjects
    {
        private GlobalObjects()
        {

        }
        private static GlobalObjects _instance;
        public static GlobalObjects Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GlobalObjects();
                }
                return _instance;
            }
        }

        public IpCamModel CamModel { get; set; } = new IpCamModel();
    }
}
