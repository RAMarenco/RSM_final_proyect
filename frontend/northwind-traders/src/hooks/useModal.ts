import { useEffect, useState } from "react";

export function useModal (setShow: (show: boolean) => void) {
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isMounted, setIsMounted] = useState<boolean>(false);
  useEffect(() => {
    setIsMounted(true);
  }, []);

  const handleClose = () => {
    if (isLoading) return;
    setIsMounted(false);
      setTimeout(() => {
        setShow(false);
      }, 300);
  };

  return {isLoading, setIsLoading, isMounted, handleClose};
}