using QbcMoleculesBusinessLogic.Data.MoleculeMath;

namespace QbcMoleculesBusinessLogic.Business.MoleculeMath
{
    public static class MoleculeMath
    {
        public static Molecule3DPoint? MiddlePoint(Molecule3DPointList pointList)
        {
            Molecule3DPoint? retval = null;
            if (pointList.Points.Count > 0)
            {
                retval = new Molecule3DPoint(pointList.Points.Sum(i => i.X) / pointList.Points.Count,
                                        pointList.Points.Sum(i => i.Y) / pointList.Points.Count,
                                        pointList.Points.Sum(i => i.Z) / pointList.Points.Count);
            }
            return retval;
        }

        public static Molecule3DPointList Translate(Molecule3DPointList pointList, Molecule3DPoint vector)
        {
            foreach (var point in pointList.Points)
            {
                point.X -= vector.X;
                point.Y -= vector.Y;
                point.Z -= vector.Z;
            }
            return pointList;
        }

        public static double Distance(Molecule3DPoint first, Molecule3DPoint second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2)
                    + Math.Pow(first.Y - second.Y, 2)
                    + Math.Pow(first.Z - second.Z, 2));
        }

        public static double MaxDistance(Molecule3DPointList pointList)
        {
            Molecule3DPoint? midpoint = MiddlePoint(pointList);
            double max = 0;
            foreach (var point in pointList.Points)
            {
                double current = Distance(midpoint, point);
                if (current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        public static double MeanDistance(Molecule3DPointList pointList)
        {
            double distance = 0;
            Molecule3DPoint? midpoint = MiddlePoint(pointList);
            foreach (var point in pointList.Points)
            {
                distance += Distance(midpoint, point);
            }
            return distance / pointList.Points.Count;
        }

        public static double StdDevDistance(Molecule3DPointList pointList)
        {
            double retval = 0.0;
            double mean = MeanDistance(pointList);
            Molecule3DPoint? midpoint = MiddlePoint(pointList);
            foreach (var point in pointList.Points)
            {
                retval += Math.Pow((Distance(point, midpoint)) - mean, 2);
            }
            int number = pointList.Points.Count - 1;
            return Math.Sqrt(retval / number);
        }
    }
}
