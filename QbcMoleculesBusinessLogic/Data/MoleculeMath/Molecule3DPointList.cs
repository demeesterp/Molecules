namespace QbcMoleculesBusinessLogic.Data.MoleculeMath
{
    public class Molecule3DPointList
    {
        public Molecule3DPointList()
        {
            Points = new List<Molecule3DPoint>();
        }

        public Molecule3DPointList(Molecule3DPointList points)
        {
            Points = (from i in points.Points select new Molecule3DPoint(i)).ToList();
        }

        public List<Molecule3DPoint> Points { get; set; }
    }
}
