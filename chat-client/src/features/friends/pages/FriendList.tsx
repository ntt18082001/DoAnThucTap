import React, { Fragment, useEffect, useState } from 'react';
import { useGetListUserNotFriendQuery } from '../friends.service';
import { SearchFriend } from 'models/friend.model';
import SkeletonFriend from 'components/Common/Client/SkeletonFriend';
import FriendItem from './FriendItem';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { selectFriends, setFriends } from '../friendSlice';

interface FriendListProps {
  id?: string;
  searchTerm: string;
}

function FriendList(props: FriendListProps) {
  const dispatch = useAppDispatch();
  const friends = useAppSelector(selectFriends);
  const [filterSearch, setFilterSearch] = useState<SearchFriend>({
    id: props.id,
    search: props.searchTerm,
  });
  const { data, isSuccess, isFetching } = useGetListUserNotFriendQuery(filterSearch);

  useEffect(() => {
    setFilterSearch({
      id: props.id,
      search: props.searchTerm,
    });
    
    if(isSuccess && data) {
      dispatch(setFriends(data));
    }
  }, [props.searchTerm, props.id, isSuccess, data, dispatch]);

  return (
    <>
      {isFetching && (
        <Fragment>
          <SkeletonFriend />
          <SkeletonFriend />
          <SkeletonFriend />
          <SkeletonFriend />
        </Fragment>
      )}
      {!isFetching &&
        friends?.map((friend) => (
          <FriendItem
            key={friend.id}
            friend={friend}
            senderId={props.id}
          />
        ))}
    </>
  );
}
export default React.memo(FriendList);
