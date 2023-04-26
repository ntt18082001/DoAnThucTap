import { List } from '@mui/material';
import React, { Fragment, useEffect, useState } from 'react';
import FindUserItem from './FindUserItem';
import SkeletonFindUserMsg from './SkeletonFindUserMsg';
import { SearchFriend } from 'models/friend.model';
import { useAppSelector } from 'app/hooks';
import { selectUserId } from 'features/auth/authSlice';
import { useGetListFriendMessageQuery } from '../message.service';

interface FindUserListProps {
  searchTerm: string;
  handleHiddenPopper: () => void;
}

function FindUserList(props: FindUserListProps) {
  const currendUserId = useAppSelector(selectUserId);
  const [filterSearch, setFilterSearch] = useState<SearchFriend>({
    id: currendUserId,
    search: props.searchTerm,
  });
  const { data, isSuccess, isFetching } = useGetListFriendMessageQuery(filterSearch);

  useEffect(() => {
    setFilterSearch({
      id: currendUserId,
      search: props.searchTerm,
    });
  }, [props.searchTerm, isSuccess, data, currendUserId]);

  return (
    <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
      {isFetching && (
        <Fragment>
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
        </Fragment>
      )}
      {!isFetching && data && data.map((friend) => <FindUserItem key={friend.id} user={friend} handleHiddenPopper={props.handleHiddenPopper} />)}
    </List>
  );
}
export default React.memo(FindUserList);
