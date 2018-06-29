using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paraphrasing.Tests
{
    public class FrenchSentenceTypeDetectorTests
    {
        [Theory]
        [InlineData("?")]
        public void GivenSentenceEndingWithQuestionMark_ShouldDetectInterrogative(string sentence)
        {
            #warning Implement
        }

        [Theory]
        [InlineData("quel")]
        public void GivenSentenceStartingWithInterrogativePronoun_ShouldDetectInterrogative(string sentence)
        {
            #warning Implement
        }
    }
}
