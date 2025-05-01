import { IPaginated } from "@/types/Pagination/pagination";
import { Dispatch, SetStateAction, useState } from "react";
import DefaultButton from "../Button/DefaultButton";

const Pagination = <T,>({data, currentPage, setCurrentPage} : {data: IPaginated<T>, currentPage: number, setCurrentPage: Dispatch<SetStateAction<number>>}) => {

  if (!data || data.totalPages <= 1) return null;

  const totalPages = data.totalPages;
  const pages = [];

  // Always add first page
  pages.push(1);

  // Determine the range of pages to show around current page
  let startPage = Math.max(2, currentPage - 1);
  let endPage = Math.min(totalPages - 1, currentPage + 1);

  // Adjust if we're near the start or end
  if (currentPage <= 3) {
    endPage = Math.min(4, totalPages - 1);
  } else if (currentPage >= totalPages - 2) {
    startPage = Math.max(totalPages - 3, 2);
  }

  // Add ellipsis if there's a gap between first page and startPage
  if (startPage > 2) {
    pages.push('...');
  }

  // Add middle pages
  for (let i = startPage; i <= endPage; i++) {
    pages.push(i);
  }

  // Add ellipsis if there's a gap between endPage and last page
  if (endPage < totalPages - 1) {
    pages.push('...');
  }

  // Always add last page if there's more than one page
  if (totalPages > 1) {
    pages.push(totalPages);
  }

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  return (
    <div className="flex justify-center items-center space-x-2">
      {pages.map((page, index) => (
        <div key={index}>
          {page === '...' ? (
            <span className="px-3 py-1 text-lg pointer-events-none">...</span>
          ) : (
            <DefaultButton
              type="button"
              onClick={() => handlePageChange(page as number)}
              className={currentPage === page ? 'bg-gray-600! text-white hover:bg-gray-600! hover:cursor-default!' : 'bg-gray-400! hover:bg-gray-700!'}
            >
              {page}
            </DefaultButton>
          )}
        </div>
      ))}
    </div>
  );
};

export default Pagination;