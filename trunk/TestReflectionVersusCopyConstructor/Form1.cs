using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ReflectionVersusCopyConstructorTestApp
{
    public partial class Form1 : Form
    {
        private Type typeToCreateViaReflection = typeof (ATypeOfAttribute<int>);
        private ATypeOfAttribute<int> objectToClone = new ATypeOfAttribute<int> ("Justin", 1);

        public Form1 ()
        {
            InitializeComponent ();
        }

        private void RunButton_Click (object sender, EventArgs e)
        {
            DateTime reflectionStartTime = DateTime.Now;
            for (int i = 0; i < IterationsNumericUpDown.Value; i++)
            {
                ConstructorInfo ci = typeToCreateViaReflection.GetConstructor (new Type[] { typeof (string), typeof (int) });
                object attributeInstance = ci.Invoke (new object[] { "Justin", i });
                if (attributeInstance == null)
                    throw new Exception ("Could not reflect attribute object");
            }
            DateTime reflectionEndTime = DateTime.Now;
            this.ReflectionDurationTextBox.Text = (reflectionEndTime - reflectionStartTime).ToString ();

            DateTime cloneStartTime = DateTime.Now;
            for (int i = 0; i < IterationsNumericUpDown.Value; i++)
            {
                ATypeOfAttribute<int> newClonedInstance = objectToClone.Clone () as ATypeOfAttribute<int>;
                if (newClonedInstance == null)
                    throw new Exception ("Could not clone attribute object");
            }
            DateTime cloneEndTime = DateTime.Now;
            this.CopyConstructorDurationTextBox.Text = (cloneEndTime - cloneStartTime).ToString ();
        }
    }

    class ATypeOfAttribute<T> : ICloneable
    {
        private string name;
        private T value;

        public ATypeOfAttribute (string name, T value)
        {
            this.name = name;
            this.value = value;
        }

        public object Clone ()
        {
            return new ATypeOfAttribute<T> (name, value);
        }
    }
}
