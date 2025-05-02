const Main = ({ children }: { children: React.ReactNode }) => {
  return <main className="flex-1 lg:px-12 flex max-h-[calc(100dvh-8rem)] overflow-y-auto">{children}</main>;
};

export default Main;
