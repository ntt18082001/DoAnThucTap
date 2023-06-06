import { Box, Button } from '@mui/material';
import React from 'react';

interface PagerItem {
  isCurrentPage: boolean;
  text: string | JSX.Element;
  page: number;
}

interface PagerProps {
  currentPage: number;
  pageSize: number;
  totalRow: number;
  handleClick: (p: number) => void;
}

const ON_EACH_SIDE = 2;

const Pager = (props: PagerProps) => {
  const { currentPage, pageSize, totalRow, handleClick } = props;
  const totalPage = Math.ceil(totalRow / pageSize);
  const currentPageNum = parseInt(String(currentPage));

  if (totalPage === 0) {
    return null;
  }

  const pagerItems: PagerItem[] = [];

  for (
    let i = Math.max(1, currentPageNum - ON_EACH_SIDE);
    i <= Math.min(currentPageNum + ON_EACH_SIDE, totalPage);
    i++
  ) {
    pagerItems.push({
      isCurrentPage: i === currentPageNum,
      text: i.toString(),
      page: i,
    });
  }

  if (currentPageNum - ON_EACH_SIDE > 1) {
    pagerItems.unshift({
      isCurrentPage: false,
      text: <>&larr;</>,
      page: 1,
    });
  }

  if (currentPageNum + ON_EACH_SIDE < totalPage) {
    pagerItems.push({
      isCurrentPage: false,
      text: <>&rarr;</>,
      page: totalPage,
    });
  }

  return (
    <Box sx={{ mt: 2, textAlign: 'end' }}>
      {pagerItems.map((pagerItem, index) => (
        <PageItem key={index} isCurrentPage={pagerItem.isCurrentPage} page={pagerItem.page} children={pagerItem.text} handleClick={handleClick} />
      ))}
    </Box>
  );
};

interface PageItemProps {
  isCurrentPage: boolean;
  page: number;
  children: string | JSX.Element;
  handleClick: (p: number) => void;
}

const PageItem = (props: PageItemProps) => {
  const { isCurrentPage, page, children, handleClick } = props;

  return (
    <Button
      size='small'
      color="secondary"
      variant={isCurrentPage ? 'contained' : 'outlined'}
      onClick={() => handleClick(page)}
      style={{ margin: '4px' }}
    >
      {children}
    </Button>
  );
};

export default Pager;
