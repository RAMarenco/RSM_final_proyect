const Main = ({ children }: { children: React.ReactNode }) => {
  return <main className="flex-1 px-12 flex max-h-[calc(100dvh-8rem)]">{children}</main>;
};

export default Main;
