import { Box, Grid } from '@mui/material';
import React from 'react';
import FriendItemProfile from './FriendItemProfile';
import SkeletonFriendProfile from './SkeletonFriendProfile';
import { useGetListFriendQuery } from '../profile.service';

function ListFriend() {
  const { data, isSuccess, isFetching } = useGetListFriendQuery();

  return (
    <Box sx={{ maxHeight: 600, overflowY: 'scroll' }}>
      <Grid container spacing={2}>
        {isFetching && (
          <>
            <SkeletonFriendProfile />
            <SkeletonFriendProfile />
            <SkeletonFriendProfile />
            <SkeletonFriendProfile />
          </>
        )}
        {!isFetching &&
          isSuccess &&
          data &&
          data.map((friend) => <FriendItemProfile key={friend.id} item={friend} />)}
      </Grid>
    </Box>
  );
}

export default React.memo(ListFriend);
