global using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResultDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ResultDto<TData>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public TData Data { get; set; }
    }
}
