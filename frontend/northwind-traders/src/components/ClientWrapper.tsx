"use client";
import { ReactNode, useEffect, useState } from "react";;
import { Toaster } from "sonner";
import Body from "./Layout/Body";
import Header from "./Layout/Header";
import Main from "./Layout/Main";
import Footer from "./Layout/Footer";
import { usePathname } from "next/navigation";

export default function ClientWrapper({ children }: { children: ReactNode }) {
  const [menuActive, setMenuActive] = useState<boolean>(false);
  const items = ["order", "customer", "employee", "shipper"];
  const pathname = usePathname();
  
  useEffect(() => {
    setMenuActive(false);
  }, [pathname])

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