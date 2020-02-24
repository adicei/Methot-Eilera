using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathParserNet;
//using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace Jarvis
{
    class Helper
    {
        static Parser P = new Parser();
        static public void addvar()
        {
            P.AddVariable("Pi",Math.PI);
        }
        public static double Function_from_string(double current_x, string formula)
        {
            P.AddVariable("x", current_x);
            double res = P.SimplifyDouble(formula);
            P.RemoveVariable("x");
            return res;
        }
        public static double Function_from_string(string formula)
        {
            //P.AddVariable("x", current_x);
            //double res = P.SimplifyDouble(formula);
            //P.RemoveVariable("x");
            return P.Simplify(formula).DoubleValue;
        }
        public static double Function_from_string(double current_x, double current_y, string formula)
        {
            P.AddVariable("x", current_x);
            P.AddVariable("y", current_y);
            double res = P.SimplifyDouble(formula);
            P.RemoveVariable("x");
            P.RemoveVariable("y");
            return res;
        }
        public static double Function_from_string(double current_x, double current_y, double current_z, string formula)
        {
            P.AddVariable("x", current_x);
            P.AddVariable("y", current_y);
            P.AddVariable("z", current_z);
            double res = P.SimplifyDouble(formula);
            P.RemoveVariable("x");
            P.RemoveVariable("y");
            P.RemoveVariable("z");
            return res;
        }
        public static double UniCorn(string formula, params Variabla[] vabla)
        {
            for (int i = 0; i < vabla.Length; i++)
            {
                P.AddVariable(vabla[i].name, vabla[i].value);
            }
            double res = P.SimplifyDouble(formula);
            for (int i = 0; i < vabla.Length; i++)
            {
                P.RemoveVariable(vabla[i].name);
            }
            return res;
        }

        //public static double Take(TextBox some_textbox)
        //{
        //    double result = 0;
        //    bool some = true;
        //    some = double.TryParse(some_textbox.Text, out result);
        //    if (some == false) some_textbox.BackColor = Color.Red;
        //    else some_textbox.BackColor = Color.White;
        //    return result;
        //}
        //public static void PutDataIntoGrid(double[][] values, DataGridView grid, int round)
        //{
        //    grid.Columns.Clear();
        //    grid.Rows.Clear();
        //    grid.ColumnCount = values.GetLength(1);
        //    for (int i = 0; i < values.GetLength(0); i++)
        //    {
        //        grid.Rows.Add();
        //    }
        //    for (int i = 0; i < values.GetLength(1); i++)
        //        for (int j = 0; j < values.GetLength(0); j++)
        //            grid[i, j].Value = Math.Round(values[i][j], round).ToString();
        //}
        //public static void PutDataIntoGrid(double[,] values, DataGridView grid, int round)
        //{
        //    grid.Columns.Clear();
        //    grid.Rows.Clear();
        //    grid.ColumnCount = values.GetLength(1);
        //    for (int i = 1; i < values.GetLength(0); i++) grid.Rows.Add();

        //    for (int i = 0; i < values.GetLength(0); i++)
        //        for (int j = 0; j < values.GetLength(1); j++)
        //            grid[j, i].Value = Math.Round(values[i, j], round).ToString();
        //}
        //public static void Highlight(int xmin, int xmax, int ymin, int ymax, DataGridView grid)
        //{
        //    for (int i = ymin; i < ymax; i++) for (int j = xmin; j < xmax; j++)
        //            grid[j, i].Selected = true;
        //}
    }
    class Variabla
    {
        public string name;
        public double value;
        public Variabla(string sourcename, double sourcevalue)

        {
            this.name = sourcename;
            this.value = sourcevalue;
        }
    }
}
