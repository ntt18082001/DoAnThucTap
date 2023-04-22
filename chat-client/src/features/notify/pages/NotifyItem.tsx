import { Avatar, Box, Button, MenuItem, Typography } from '@mui/material';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { NotifyModel } from 'models/notify.model';
import React, { useEffect } from 'react';
import { useAcceptRequestNotifyMutation, useCancelRequestMutation } from '../notify.service';
import { setAcceptRequestNotify, setCancelNotify } from '../notifySlice';
import { setAcceptRequest, setCancelReceiverRequest } from 'features/friends/friendSlice';
import { toast } from 'react-toastify';
import { selectUserId } from 'features/auth/authSlice';

interface NotifyItemProps {
  notify: NotifyModel;
}

function NotifyItem(props: NotifyItemProps) {
  const dispatch = useAppDispatch();
  const [cancelRequest, { data, isSuccess }] = useCancelRequestMutation();
  const [acceptRequest, acceptRequestResult] = useAcceptRequestNotifyMutation();
  const userId = useAppSelector(selectUserId);

  const handleCancelRequest = async () => {
    try {
      await cancelRequest(props.notify.id).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  const handleAcceptRequest = async () => {
    try {
      await acceptRequest(props.notify.id).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setCancelNotify(props.notify.id));
      dispatch(setCancelReceiverRequest(data));
      toast.error(`Đã từ chối lời mời kết bạn của '${props.notify.fullName}'`);
    }
  }, [data, dispatch, isSuccess, props.notify.id, props.notify.fullName]);

  useEffect(() => {
    if (acceptRequestResult.isSuccess && acceptRequestResult.data) {
      dispatch(setAcceptRequestNotify(props.notify.id));
      dispatch(setAcceptRequest(acceptRequestResult.data));
      toast.success(`Đã chấp nhận lời mời kết bạn từ '${props.notify.fullName}'`);
    }
  }, [acceptRequestResult.data, acceptRequestResult.isSuccess, dispatch, props.notify.fullName, props.notify.id]);

  return (
    <MenuItem>
      <Box sx={{ display: 'flex', width: '100%', padding: '8px' }}>
        <Box sx={{ mr: 1 }}>
          <Avatar
            alt={props.notify.fullName}
            src={`${baseURL}/${props.notify.avatar}`}
            sx={{ width: '70px !important', height: '70px !important' }}
          />
        </Box>
        <Box>
          {props.notify.receiverId === userId && (
            <Box>
              <Typography
                component="strong"
                sx={{ fontWeight: 'bold', wordWrap: 'break-word', wordBreak: 'break-word' }}
              >
                {props.notify.fullName}
              </Typography>
              <Typography component="span" sx={{ wordWrap: 'break-word', wordBreak: 'break-word' }}>
                {' '}
                đã gửi cho bạn lời mời kết bạn.
              </Typography>
            </Box>
          )}
          {props.notify.senderId === userId && (
            <Box>
              <Typography component="strong" sx={{ fontWeight: 'bold'}}>{props.notify.fullName}</Typography>
              <Typography component="span"> đã chấp nhận lời mời kết bạn của bạn.</Typography>
            </Box>
          )}
          <Typography>{props.notify.mutualFriends} bạn chung</Typography>
          {props.notify.receiverId === userId && (
            <Box sx={{ display: 'flex' }}>
              {!props.notify.isCancel && !props.notify.isAccept && (
                <>
                  <Button
                    variant="contained"
                    color="primary"
                    sx={{ mr: 2 }}
                    onClick={handleAcceptRequest}
                  >
                    Chấp nhận
                  </Button>
                  <Button variant="contained" color="error" onClick={handleCancelRequest}>
                    Từ chối
                  </Button>
                </>
              )}
              {props.notify.isAccept && !props.notify.isCancel && (
                <Typography>Đã chấp nhận lời mời kết bạn.</Typography>
              )}
              {props.notify.isCancel && !props.notify.isAccept && (
                <Typography>Đã hủy lời mời kết bạn.</Typography>
              )}
            </Box>
          )}
        </Box>
      </Box>
    </MenuItem>
  );
}
export default React.memo(NotifyItem);
