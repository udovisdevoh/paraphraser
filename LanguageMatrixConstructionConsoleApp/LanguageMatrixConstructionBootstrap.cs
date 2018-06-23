using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovMatrices;
using Unity;
using Unity.Resolution;

namespace LanguageMatrixConstruction
{
    public class LanguageMatrixConstructionBootstrap
    {
        #region Constants
        private const string LanguageMatrixBuilder = "LanguageMatrixBuilder";

        private const string BinaryMarkovMatrixLoader = "BinaryMarkovMatrixLoader";

        private const string BinaryMarkovMatrixSaver = "BinaryMarkovMatrixSaver";

        private const string TextMarkovMatrixLoader = "TextMarkovMatrixLoader";

        private const string LanguageDictionaryFileMatrixLoader = "LanguageDictionaryFileMatrixLoader";

        private const string NormalizedTextMarkovMatrixLoader = "NormalizedTextMarkovMatrixLoader";

        private const string MarkovMatrixNormalizer = "MarkovMatrixNormalizer";
        #endregion

        #region Members
        private IUnityContainer container;
        #endregion

        #region Constructors
        public LanguageMatrixConstructionBootstrap()
        {
            this.container = new UnityContainer();

            container.RegisterType<ILanguageMatrixBuilder, LanguageMatrixBuilder>(LanguageMatrixBuilder);

            // FromBinary
            container.RegisterType<IMarkovMatrixLoader<double>, BinaryMarkovMatrixLoader>(BinaryMarkovMatrixLoader);
            container.RegisterType<IMarkovMatrixSaver<double>, BinaryMarkovMatrixSaver>(BinaryMarkovMatrixSaver);

            // FromText
            container.RegisterType<IMarkovMatrixLoader<ulong>, TextMarkovMatrixLoader>(TextMarkovMatrixLoader);
            container.RegisterType<IMarkovMatrixLoader<ulong>, LanguageDictionaryFileMatrixLoader>(LanguageDictionaryFileMatrixLoader);
            container.RegisterType<IMarkovMatrixLoader<double>, NormalizedTextMarkovMatrixLoader>(NormalizedTextMarkovMatrixLoader);

            // Normalizer
            container.RegisterType<IMarkovMatrixNormalizer, MarkovMatrixNormalizer>(MarkovMatrixNormalizer);
        }
        #endregion

        public IMarkovMatrixLoader<ulong> BuildLanguageDictionaryFileMatrixLoader()
        {
            ResolverOverride[] parameters = new ResolverOverride[]
            {
                new ParameterOverride("isRemoveDiacritics", true)
            };
            return this.container.Resolve<IMarkovMatrixLoader<ulong>>(LanguageDictionaryFileMatrixLoader, parameters);
        }

        public IMarkovMatrixNormalizer BuildMarkovMatrixNormalizer()
        {
            return this.container.Resolve<IMarkovMatrixNormalizer>(MarkovMatrixNormalizer);
        }

        private IMarkovMatrixSaver<double> BuildBinaryMarkovMatrixSaver()
        {
            return this.container.Resolve<IMarkovMatrixSaver<double>>(BinaryMarkovMatrixSaver);
        }

        private IMarkovMatrixLoader<double> BuildNormalizedTextMarkovMatrixLoader()
        {
            IMarkovMatrixLoader<ulong> internalMarkovMatrixLoader = this.BuildTextMarkovMatrixLoader();
            IMarkovMatrixNormalizer markovMatrixNormalizer = this.BuildMarkovMatrixNormalizer();

            ResolverOverride[] parameters = new ResolverOverride[]
            {
                new ParameterOverride("internalMarkovMatrixLoader", internalMarkovMatrixLoader),
                new ParameterOverride("markovMatrixNormalizer", markovMatrixNormalizer),
            };
            return this.container.Resolve<IMarkovMatrixLoader<double>>(NormalizedTextMarkovMatrixLoader, parameters);
        }

        private IMarkovMatrixLoader<ulong> BuildTextMarkovMatrixLoader()
        {
            return this.container.Resolve<IMarkovMatrixLoader<ulong>>(TextMarkovMatrixLoader);
        }

        public ILanguageMatrixBuilder BuildLanguageMatrixBuilder()
        {
            IMarkovMatrixLoader<double> normalizedTextMarkovMatrixLoader = this.BuildNormalizedTextMarkovMatrixLoader();
            IMarkovMatrixSaver<double> binaryMarkovMatrixSaver = this.BuildBinaryMarkovMatrixSaver();

            ResolverOverride[] parameters = new ResolverOverride[]
            {
                new ParameterOverride("normalizedTextMarkovMatrixLoader", normalizedTextMarkovMatrixLoader),
                new ParameterOverride("binaryMarkovMatrixSaver", binaryMarkovMatrixSaver)
            };
            ILanguageMatrixBuilder languageMatrixBuilder = container.Resolve<ILanguageMatrixBuilder>(LanguageMatrixBuilder, parameters);
            return languageMatrixBuilder;
        }
    }
}
