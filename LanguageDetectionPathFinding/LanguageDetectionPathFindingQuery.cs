using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class LanguageDetectionPathFindingQuery : IPathfindingQuery<LanguageDetectionState>
    {
        #region Members
        private LanguageDetectionState source;

        private LanguageDetectionState destination;

        private bool isTimeOut = false;
        #endregion

        #region Constructors
        public LanguageDetectionPathFindingQuery(LanguageDetectionState source, LanguageDetectionState destination)
        {
            this.source = source;
            this.destination = destination;
        }
        #endregion

        #region Properties
        public LanguageDetectionState Source
        {
            get { return this.source; }
            set { this.source = value; }
        }

        public LanguageDetectionState Destination
        {
            get { return this.destination; }
            set { this.destination = value; }
        }

        public bool IsTimeOut
        {
            get { return this.isTimeOut; }
            set { this.isTimeOut = value; }
        }
        #endregion

        public float EstimateCostToDestination(LanguageDetectionState state)
        {
            #warning Implement
            throw new NotImplementedException();
        }

        public void GetAdjacentStates(PathNode<LanguageDetectionState> node, List<AdjacentState<LanguageDetectionState>> adjacentStates)
        {
            #warning Implement
            throw new NotImplementedException();
        }
    }
}
