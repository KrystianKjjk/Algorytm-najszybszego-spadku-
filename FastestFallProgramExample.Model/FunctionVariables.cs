using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.Model
{
    public class FunctionVariables
    {
        public ObservableCollection<Variable> Variables { get; set; }
        public double Result { get; set; }
        public FunctionVariables()
        {
            Variables = new ObservableCollection<Variable>(); 
        }
    }
}
