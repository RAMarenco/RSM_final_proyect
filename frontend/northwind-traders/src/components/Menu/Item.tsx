
import Link from "next/link";

export const Item = ({
  title,
  Icon,
  link,
  className,
}: {
  title: string;
  Icon?: React.ReactNode | undefined;
  link: string;
  className?: string;
}) => {
  return (
    <li className={className}>
      <Link
        href={link}
        className="inline-flex items-center justify-center py-4 border-b-2 border-transparent rounded-t-lg text-black hover:text-gray-800 hover:border-gray-800 ease-in transition-all duration-200 group"
      >
        {Icon}
        {title}
      </Link>
    </li>
  );
};
