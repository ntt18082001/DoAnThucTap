import React, { useEffect } from 'react';
import {
  Box,
  Card,
  CardActions,
  CardContent,
  CardMedia,
  Grid,
  Typography,
} from '@mui/material';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import useMode from 'hooks/useMode';
import { baseURL } from 'endpoints';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import { AddFriend, FriendModel, Unfriend } from 'models/friend.model';
import { CssLoadingButton } from 'utils/CssTextField';
import { useAcceptRequestMutation, useAddFriendMutation, useCancelRequestFromReceiverMutation, useCancelRequestMutation, useUnfriendMutation } from '../friends.service';
import { setAcceptRequest, setCancelReceiverRequest, setCancelRequest, setFriendSendRequest, setUnfriend } from '../friendSlice';
import { toast } from 'react-toastify';
import { setAcceptRequestNotify, setCancelNotify } from 'features/notify/notifySlice';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';

interface FriendItemProps {
  friend: FriendModel;
  senderId?: string;
}

function FriendItem(props: FriendItemProps) {
  const dispatch = useAppDispatch();
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const darkmode = useMode(isDarkmode);
  const [addFriend, { data, isLoading, isSuccess }] = useAddFriendMutation();
  const [cancelRequest, cancelRequestResult] = useCancelRequestMutation();
  const [cancelRequestFromReceiver, cancelRequestFromReceiverResult] = useCancelRequestFromReceiverMutation();
  const [acceptRequest, acceptRequestResult] = useAcceptRequestMutation();
  const [unfriend, unfriendResult] = useUnfriendMutation();

  const handleAddFriend = async (userId: string) => {
    try {
      const sender: AddFriend = {
        senderId: props.senderId,
        receiverId: userId,
      };
      await addFriend(sender).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  const handleCancelRequest = async (userId: string) => {
    try {
      const sender: AddFriend = {
        senderId: props.senderId,
        receiverId: userId,
      };
      await cancelRequest(sender).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  const handleAcceptRequest = async (userId: string) => {
    try {
      const sender: AddFriend = {
        senderId: userId,
        receiverId: props.senderId
      }
      await acceptRequest(sender).unwrap();
    } catch(error) {
      console.log(error);
    }
  }

  const handleCancelRequestFromReceiver = async (userId: string) => {
    try {
      const sender: AddFriend = {
        senderId: userId,
        receiverId: props.senderId,
      };
      await cancelRequestFromReceiver(sender).unwrap();
    } catch (error) {
      console.log(error);
    }
  }

  const handleUnfriend = async (userId: string) => {
    try {
      const sender: Unfriend = {
        senderId: props.senderId,
        receiverId: userId
      };
      await unfriend(sender).unwrap();
    } catch(error) {
      console.log(error)
    }
  }

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setFriendSendRequest(data));
      toast.success(`Đã gửi lời mời kết bạn tới '${props.friend.fullName}'`);
    }
  }, [isSuccess, data, dispatch, props.friend.fullName]);

  useEffect(() => {
    if (cancelRequestResult.isSuccess && cancelRequestResult.data) {
      dispatch(setCancelRequest(cancelRequestResult.data));
      toast.error(`Đã hủy lời mời kết bạn với '${props.friend.fullName}'`);
    }
  }, [cancelRequestResult.isSuccess, cancelRequestResult.data, dispatch, props.friend.fullName, props.friend.id]);

  useEffect(() => {
    if (cancelRequestFromReceiverResult.isSuccess && cancelRequestFromReceiverResult.data) {
      dispatch(setCancelReceiverRequest(cancelRequestFromReceiverResult.data.senderId));
      dispatch(setCancelNotify(cancelRequestFromReceiverResult.data.notifyId));
      toast.error(`Đã từ chối lời mời kết bạn của '${props.friend.fullName}'`);
    }
  }, [cancelRequestFromReceiverResult.isSuccess, cancelRequestFromReceiverResult.data, dispatch, props.friend.fullName, props.friend.id]);

  useEffect(() => {
    if(acceptRequestResult.isSuccess && acceptRequestResult.data) {
      dispatch(setAcceptRequest(acceptRequestResult.data.senderId));
      dispatch(setAcceptRequestNotify(acceptRequestResult.data.notifyId));
      toast.success(`Đã chấp nhận lời mời kết bạn từ '${props.friend.fullName}'`);
    }
  }, [acceptRequestResult.data, acceptRequestResult.isSuccess, dispatch, props.friend.fullName]);

  useEffect(() => {
    if(unfriendResult.isSuccess && unfriendResult.data) {
      toast.info(`Đã hủy kết bạn với '${props.friend.fullName}'`);
    }
  }, [dispatch, props.friend.fullName, props.friend.id, props.senderId, unfriendResult.data, unfriendResult.isSuccess]);

  return (
    <Grid item xs={3} md={3} sm={6}>
      <Card
        sx={{
          maxWidth: 300,
          bgcolor: darkmode?.bgColor,
          color: darkmode?.color,
        }}
      >
        <CardMedia
          sx={{
            height: 300,
          }}
          image={`${baseURL}/${props.friend.avatar}`}
          title={props.friend.fullName}
        ></CardMedia>
        <CardContent>
          <Typography gutterBottom variant="h5" component="div" textAlign="center">
            {props.friend.fullName}
          </Typography>
          <Typography gutterBottom component="div" textAlign="center">
            {props.friend.mutualFriends} bạn chung
          </Typography>
        </CardContent>
        <CardActions>
          {!props.friend.isSendRequest &&
            !props.friend.isReceiverRequest &&
            !props.friend.isFriendShip && (
              <CssLoadingButton
                color="secondary"
                loadingPosition="start"
                startIcon={<PersonAddIcon />}
                variant="contained"
                loading={isLoading}
                sx={{ margin: '0 auto' }}
                onClick={() => handleAddFriend(props.friend.id)}
              >
                Kết bạn
              </CssLoadingButton>
            )}
          {props.friend.isSendRequest &&
            !props.friend.isReceiverRequest &&
            !props.friend.isFriendShip && (
              <CssLoadingButton
                color="error"
                loadingPosition="start"
                startIcon={<PersonRemoveIcon />}
                variant="contained"
                loading={cancelRequestResult.isLoading}
                sx={{ margin: '0 auto' }}
                onClick={() => handleCancelRequest(props.friend.id)}
              >
                Hủy lời mời
              </CssLoadingButton>
            )}
          {props.friend.isReceiverRequest &&
            !props.friend.isSendRequest &&
            !props.friend.isFriendShip && (
              <Box sx={{ width: '100%', display: 'flex', justifyContent: 'space-evenly' }}>
                <CssLoadingButton
                  color="secondary"
                  loadingPosition="start"
                  startIcon={<PersonAddIcon />}
                  variant="contained"
                  loading={acceptRequestResult.isLoading}
                  onClick={() => handleAcceptRequest(props.friend.id)}
                >
                  Chấp nhận
                </CssLoadingButton>
                <CssLoadingButton
                  color="error"
                  loadingPosition="start"
                  startIcon={<PersonRemoveIcon />}
                  variant="contained"
                  loading={cancelRequestFromReceiverResult.isLoading}
                  onClick={() => handleCancelRequestFromReceiver(props.friend.id)}
                >
                  Từ chối
                </CssLoadingButton>
              </Box>
            )}
          {props.friend.isFriendShip &&
            !props.friend.isSendRequest &&
            !props.friend.isReceiverRequest && (
              <>
                <CssLoadingButton
                  color="error"
                  loadingPosition="start"
                  startIcon={<PersonRemoveIcon />}
                  variant="contained"
                  sx={{ margin: '0 auto' }}
                  loading={unfriendResult.isLoading}
                  onClick={() => handleUnfriend(props.friend.id)}
                >
                  Hủy kết bạn
                </CssLoadingButton>
              </>
            )}
        </CardActions>
      </Card>
    </Grid>
  );
}
export default React.memo(FriendItem);
