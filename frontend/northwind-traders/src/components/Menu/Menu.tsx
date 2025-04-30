import { Item } from "./Item";

export const Menu = ({
  items,
  menuActive
}: {
  items: string[];
  menuActive: boolean;
}) => {
  return (
    <div className={`max-lg:absolute max-lg:flex max-lg:flex-col max-lg:min-h-full max-lg:w-full max-lg:z-10 max-lg:top-0 ease-in-out duration-500 ${menuActive ? `max-lg:-left-0` : `max-lg:-left-full`} border-gray-200 max-lg:pt-23 max-lg:pb-8 max-lg:max-w-sm max-[30rem]:max-w-full flex flex-1 flex-row justify-center max-lg:bg-gray-100`}>
      <ul className="flex max-lg:flex-col max-lg:px-12 flex-wrap text-sm font-medium text-gray-500 gap-8 flex-1 relative">
        {items.length > 0
          ? items.map((item, index) => (
              <Item
                title={item.toLocaleUpperCase()}
                link={`/${item}`}
                key={index}
              />
            ))
          : null}
      </ul>
    </div>
  );
};