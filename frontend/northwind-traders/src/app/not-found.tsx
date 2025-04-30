import Link from 'next/link';

const Custom404 = () => {
  return (
    <div className="flex flex-col items-center justify-center w-full flex-1">
      <h1 className="text-6xl font-bold text-gray-800">404</h1>
      <p className="text-lg text-gray-600">Page Not Found</p>
      <Link href="/" className="mt-4 text-blue-600 hover:underline">
        Go back to Home
      </Link>
    </div>
  );
};

export default Custom404;