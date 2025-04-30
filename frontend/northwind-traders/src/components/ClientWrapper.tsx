"use client";
import { ReactNode, useState } from "react";;
import { Toaster } from "sonner";
import Body from "./Layout/Body";
import Header from "./Layout/Header";
import Main from "./Layout/Main";
import Footer from "./Layout/Footer";

export default function ClientWrapper({ children }: { children: ReactNode }) {
  const [menuActive, setMenuActive] = useState<boolean>(false);
  const items = ["Orders", "Customers", "Employees", "Shippers"];

  return (
    <Body>
      <Toaster richColors/>
      <div className={`${menuActive ? "max-lg:bg-gray-900/50 pointer-events-auto" : "pointer-events-none"} absolute h-full w-full z-10 ease-in-out duration-500`}></div>
      <Header
        items={items}
        setMenuActive={setMenuActive}
        menuActive={menuActive}
      />      
      <Main>{children}</Main>
      <Footer />
    </Body>
  );
}