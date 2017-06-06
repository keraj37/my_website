using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_website.Controllers.MandelbrotSet.Solution01
{
    public class ComplexPoint
    {
        public double x;
        public double y;

        public ComplexPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double DoModulus()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public double DoModulusSq()
        {
            return x * x + y * y;
        }

        public ComplexPoint DoCmplxSq()
        {
            ComplexPoint result = new ComplexPoint(0, 0);
            result.x = x * x - y * y;
            result.y = 2 * x * y;

            return result;
        }

        public ComplexPoint DoCmplxAdd(ComplexPoint arg)
        {
            x += arg.x;
            y += arg.y;

            return this;
        }

        public ComplexPoint DoCmplxSqPlusConst(ComplexPoint arg)
        {
            ComplexPoint result = new ComplexPoint(0, 0);
            result.x = x * x - y * y;
            result.y = 2 * x * y;
            result.x += arg.x;
            result.y += arg.y;
            return result;
        }
    }
}
