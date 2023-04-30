import { List } from '@mui/material';
import React, { Fragment, useEffect, useState } from 'react';
import FindUserItem from './FindUserItem';
import SkeletonFindUserMsg from './SkeletonFindUserMsg';
import { SearchFriend } from 'models/friend.model';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { selectUserId } from 'features/auth/authSlice';
import { useGetListFriendMessageQuery } from '../message.service';
import { selectListFindUser, setListFindUser } from '../messageSlice';

interface FindUserListProps {
  searchTerm: string;
  handleHiddenPopper: () => void;
}

function FindUserList(props: FindUserListProps) {
  const dispatch = useAppDispatch();
  const listFindUser = useAppSelector(selectListFindUser);
  const currendUserId = useAppSelector(selectUserId);
  const [filterSearch, setFilterSearch] = useState<SearchFriend>({
    id: currendUserId,
    search: props.searchTerm,
  });
  const { data, isSuccess } = useGetListFriendMessageQuery(filterSearch);

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setListFindUser(data));
    }
    setFilterSearch({
      id: currendUserId,
      search: props.searchTerm,
    });
  }, [props.searchTerm, isSuccess, data, currendUserId, dispatch]);

  return (
    <List sx={{ width: '100%', bgcolor: 'background.paper' }}>
      {listFindUser.length === 0 ? (
        <Fragment>
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
          <SkeletonFindUserMsg />
        </Fragment>
      ) : (
        listFindUser.map((friend) => (
          <FindUserItem
            key={friend.id}
            user={friend}
            handleHiddenPopper={props.handleHiddenPopper}
          />
        ))
      )}
    </List>
  );
}
export default React.memo(FindUserList);
