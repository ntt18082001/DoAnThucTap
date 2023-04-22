import { Box, Grid, Typography } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import React from 'react';
import { CssLoadingButton } from 'utils/CssTextField';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import useMode from 'hooks/useMode';
import { FriendModel, Unfriend } from 'models/friend.model';
import { useUnfriendProfileMutation } from '../profile.service';
import { selectUserId } from 'features/auth/authSlice';

interface FriendItemProfileProps {
  item: FriendModel;
}

function FriendItemProfile(props: FriendItemProfileProps) {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const darkmode = useMode(isDarkmode);
  const userId = useAppSelector(selectUserId);
  const [unfriend, { isLoading }] = useUnfriendProfileMutation();

  const handleUnfriend = async () => {
    try {
      const data: Unfriend = {
        senderId: userId,
        receiverId: props.item.id
      };
      await unfriend(data).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Grid item xs={6} sm={6}>
      <Box sx={{ display: 'flex', border: darkmode?.borderColor, borderRadius: '5px' }}>
        <Box sx={{ width: '30%', height: '100px', overflow: 'hidden', mr: 2 }}>
          <img
            alt={props.item.fullName}
            src={`${baseURL}/${props.item.avatar}`}
            style={{ width: '100%' }}
          />
        </Box>
        <Box sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
          <Typography component="strong" sx={{ fontWeight: 'bold' }}>
            {props.item.fullName}
          </Typography>
          <Typography component="div">{props.item.mutualFriends} bạn chung</Typography>
          <CssLoadingButton
            color="error"
            loadingPosition="start"
            startIcon={<PersonRemoveIcon />}
            variant="contained"
            sx={{ margin: '0 auto' }}
            loading={isLoading}
            onClick={handleUnfriend}
          >
            Hủy kết bạn
          </CssLoadingButton>
        </Box>
      </Box>
    </Grid>
  );
}

export default React.memo(FriendItemProfile);
