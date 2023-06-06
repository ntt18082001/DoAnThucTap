import { Paged } from 'models/paged.model';
import React, { useEffect, useState } from 'react';
import { useGetAllUserQuery } from './user.service';
import {
  Avatar,
  Button,
  Pagination,
  PaginationItem,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableFooter,
  TableHead,
  TableRow,
  Typography,
} from '@mui/material';
import { baseURL } from 'endpoints';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import useMode from 'hooks/useMode';
import Pager from 'utils/Pager';

interface UserProps {
  searchTerm: string;
}

function UserList(props: UserProps) {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const darkmode = useMode(isDarkmode);
  const [paged, setPaged] = useState<Paged>({
    name: props.searchTerm,
    page: 1,
  });
  const { data, isSuccess, isFetching } = useGetAllUserQuery(paged);

  const handleNextPage = (page: number) => {
    setPaged((prev) => ({
      ...prev,
      page,
    }));
  };

  useEffect(() => {
    let prevSearchTerm = props.searchTerm;
    if (prevSearchTerm !== paged.name) {
      setPaged((prev) => ({
        ...prev,
        name: prevSearchTerm,
      }));
    }
  }, [paged.name, props.searchTerm]);

  useEffect(() => {
  }, [isSuccess, data, paged]);

  return (
    <>
      {isFetching && <Typography component="span">Loading...</Typography>}
      {!isFetching && data && (
        <>
          <TableContainer component={Paper} sx={{ backgroundColor: darkmode?.bgColor }}>
            <Table sx={{ minWidth: 650 }} aria-label="caption table">
              <caption style={{ color: darkmode?.color }}>
                Danh sách này có tổng cộng {data?.totalItems} bản ghi
              </caption>
              <TableHead>
                <TableRow>
                  <TableCell sx={{ color: darkmode?.color }}>Id</TableCell>
                  <TableCell sx={{ color: darkmode?.color }}>Avatar</TableCell>
                  <TableCell sx={{ color: darkmode?.color }}>Họ tên</TableCell>
                  <TableCell sx={{ color: darkmode?.color }}>Email</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {data?.data.map((user) => (
                  <TableRow key={user.id}>
                    <TableCell sx={{ color: darkmode?.color }}>{user.id}</TableCell>
                    <TableCell sx={{ color: darkmode?.color }}>
                      <Avatar src={`${baseURL}/${user.avatar}`} />
                    </TableCell>
                    <TableCell component="th" scope="row" sx={{ color: darkmode?.color }}>
                      {user.fullName}
                    </TableCell>
                    <TableCell sx={{ color: darkmode?.color }}>{user.email}</TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <Pager
            currentPage={data.page}
            pageSize={data.size}
            totalRow={data.totalItems}
            handleClick={handleNextPage}
          />
        </>
      )}
    </>
  );
}
export default React.memo(UserList);
