namespace TexasHoldemCalculator.Interfaces.Model
{
    public interface ISimple3DVector
    {
        /// <summary>
        /// X-axis coordinate
        /// </summary>
        double X { get; }

        /// <summary>
        /// Y-axis coordinate
        /// </summary>
        double Y { get; }

        /// <summary>
        /// Z-axis coordinate
        /// </summary>
        double Z { get; }

        /// <summary>
        /// Get Magnitude of vector
        /// </summary>
        double Magnitude { get; }

        /// <summary>
        /// Override the ToString method to display vector in suitable format:
        /// </summary>
        string ToString();

        /// <summary>
        /// Override the Object.Equals(object o) method:
        /// </summary>
        bool Equals(object o);

        /// <summary>
        /// Override the Object.Equals(object o) method:
        /// </summary>
        int GetHashCode();
    }
}