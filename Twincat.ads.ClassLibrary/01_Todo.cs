using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    //############
    public class Todo
    {
        public int Id { get; set; }             //임의에 순서 
        public string HandleName { get; set; }  //핸들의 이름
        public uint HandleValue { get; set; }   //핸들의 주소값  Twincat.ads.에서는 uint로 반환함 
    }

}
