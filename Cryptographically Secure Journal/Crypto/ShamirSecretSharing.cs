using SecretSharingDotNet.Cryptography;
using SecretSharingDotNet.Math;
using System.Linq;
using System.Numerics;

namespace CryptographicallySecureJournal.Crypto
{
    public static class ShamirSecretSharing
    {
        public const int MinNumOfShares = 3;
        public const int TotalShares = 4;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shares">Tuple of X and Y points array</param>
        /// <returns></returns>
        public static byte[] ReconstructSecret((int, byte[])[] shares)
        {
            ExtendedEuclideanAlgorithm<BigInteger> gcd = new ExtendedEuclideanAlgorithm<BigInteger>();
            ShamirsSecretSharing<BigInteger> combine = new ShamirsSecretSharing<BigInteger>(gcd);
            FinitePoint<BigInteger>[] points = shares.Select
            (val =>
            {
                (int x, byte[] yBytes) = val;
                return new FinitePoint<BigInteger>(
                     new BigIntCalculator(new BigInteger(x)),
                     new BigIntCalculator(yBytes));
            }).ToArray();
            return combine.Reconstruction(points).ToByteArray();
        }

        public static byte[][] SplitSecret(byte[] secret)
        {
            ExtendedEuclideanAlgorithm<BigInteger> gcd = new ExtendedEuclideanAlgorithm<BigInteger>();
            //// Create Shamir's Secret Sharing instance with BigInteger
            ShamirsSecretSharing<BigInteger> split = new ShamirsSecretSharing<BigInteger>(gcd);

            //// Minimum number of shared secrets for reconstruction: 4
            //// Maximum number of shared secrets: 10
            //// Attention: The password length changes the security level set by the ctor
            Shares<BigInteger> shares = split.MakeShares(MinNumOfShares,
                TotalShares, secret, 15);

            return shares.Select(point => point.Y.ByteRepresentation.ToArray()).ToArray();
        }


    }
}
