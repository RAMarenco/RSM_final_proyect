import { ReactNode } from "react";

export interface ModalContainerProps {
  children: ReactNode;
  isMounted: boolean;
}

export interface ModalProps<T = undefined> {
  setShowModal: (show: boolean) => void;
  initialState?: T extends undefined ? never : T;
}

export interface ModalHeaderProps {
  title: string;
  onClose: () => void;
  Icon?: React.ReactNode | undefined;
}
