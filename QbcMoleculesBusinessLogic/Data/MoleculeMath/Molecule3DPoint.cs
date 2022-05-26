namespace QbcMoleculesBusinessLogic.Data.MoleculeMath
{
    public class Molecule3DPoint
    {
        public Molecule3DPoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Molecule3DPoint(Molecule3DPoint point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }
    }
}
