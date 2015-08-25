namespace MW
{
    struct PointF
    {
        public float x;
        public float y;
    }

    class MWLocation
    {
        public PointF p1;
        public PointF p2;
        public PointF p3;
        public PointF p4;

        public PointF[] points;

        public MWLocation(float[] _points)
        {

            points = new PointF[4];

            for (int i = 0; i < 4; i++)
            {
                points[i] = new PointF();
                points[i].x = _points[i * 2];
                points[i].y = _points[i * 2 + 1];
            }
            p1 = new PointF();
            p2 = new PointF();
            p3 = new PointF();
            p4 = new PointF();

            p1.x = _points[0];
            p1.y = _points[1];
            p2.x = _points[2];
            p2.y = _points[3];
            p3.x = _points[4];
            p3.y = _points[5];
            p4.x = _points[6];
            p4.y = _points[7];
        }
    }
}
