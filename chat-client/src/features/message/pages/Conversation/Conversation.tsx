import { Grid } from '@mui/material';
import React, { useEffect } from 'react';

import { useAppDispatch, useAppSelector } from '../../../../app/hooks';
import { selectSelectedUser, setIsScrollFalse, setMessage } from '../../messageSlice';
import ConversationContent from './ConversationContent';
import ConversationFormMessage from './ConversationFormMessage';
import ConversationTitle from './ConversationTitle';
import { selectUserId } from 'features/auth/authSlice';
import { useSendMessageMutation } from 'features/message/message.service';

type Props = {};

const Conversation = (props: Props) => {
  const dispatch = useAppDispatch();
  const currendUserId = useAppSelector(selectUserId);
  const selectedUser = useAppSelector(selectSelectedUser);
  const selectConversation = useAppSelector((state) =>
    state.message.conversations.find(
      (item) =>
        (item.userId === currendUserId && item.friendId === selectedUser?.id) ||
        (item.userId === selectedUser?.id && item.friendId === currendUserId)
    )
  );
  const [sendMessage, { data, isSuccess }] = useSendMessageMutation();

  const handleSubmitMessage = async (message: string, file?: File) => {
    try {
      if (message === '' && file === undefined) {
        return;
      }
			const formData = new FormData();
      if(currendUserId) {
				formData.append('userId', currendUserId);
			}
      if(selectedUser?.id) {
				formData.append('friendId', selectedUser?.id);
			}
      if (file) {
				formData.append('file', file);
      }
			formData.append('content', message);
      await sendMessage(formData).unwrap();
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setMessage(data));
      dispatch(setIsScrollFalse());
    }
  }, [data, dispatch, isSuccess]);

  return (
    <>
      <Grid
        container
        direction="column"
        justifyContent="space-between"
        alignItems="stretch"
        sx={{ height: '100%' }}
      >
        <ConversationTitle user={selectedUser} />
        <ConversationContent conversations={selectConversation} />
        <ConversationFormMessage onSubmit={handleSubmitMessage} />
      </Grid>
    </>
  );
};

export default Conversation;
