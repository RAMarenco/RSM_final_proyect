const Footer = () => {
  const appTitle = process.env.NEXT_PUBLIC_APP_TITLE;
  return (
    <footer className="p-4 text-center">
      <p>
        © {new Date().getFullYear()} {appTitle}. All rights reserved.
      </p>
    </footer>
  );
};

export default Footer;
