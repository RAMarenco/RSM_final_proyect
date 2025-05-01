"use client"
import Link from "next/link";
import TransparentButton from "../Button/TransparentButton";
import { Menu } from "../Menu/Menu";
import Image from "next/image";
import { Bars3Icon, XMarkIcon } from "@heroicons/react/16/solid";

const Header = ({
  items,
  setMenuActive,
  menuActive
}: {
  items: string[];
  setMenuActive: (value: boolean) => void;
  menuActive: boolean;
}) => {
  return (
    <header className="flex flex-row justify-between py-2 px-6">
      <Link href="/" className="z-20">
        <Image src="/Logo.png" width={200} height={55.47} alt="Northwind traders logo"/>
      </Link>
      <TransparentButton
        type="button"
        className={`hidden bg-gray-100 max-lg:flex absolute size-10 right-4 shadow-lg items-center justify-center border-transparent rounded-full hover:text-gray-600 z-20`}
        onClick={() => setMenuActive(!menuActive)}
      >
        {menuActive ? (<XMarkIcon className="h-5 w-5"/>) : (<Bars3Icon className="h-5 w-5"/>)}
      </TransparentButton>
      <div className={`ease-in-out duration-500 max-lg:bg-gray-900/50`}>
        <Menu items={items} menuActive={menuActive}/>
      </div>
    </header>
  );
};

export default Header;