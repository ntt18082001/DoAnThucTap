import { Box, CircularProgress, Grid, Typography } from '@mui/material';
import React, { useEffect, useRef, useState } from 'react';

import { useAppDispatch, useAppSelector } from '../../../../app/hooks';
import { ConversationModel, GetMoreMessage } from '../../../../models/messages.model';
import ConversationMessage from './ConversationMessage';
import { selectUserId } from 'features/auth/authSlice';
import { useGetMoreMessageMutation } from 'features/message/message.service';
import {
  selectIsScroll,
  setIsScrollTrue,
  setListMessageToConversation,
} from 'features/message/messageSlice';

interface Props {
  conversations?: ConversationModel;
}

const ConversationContent = (props: Props) => {
  const dispatch = useAppDispatch();
  const [allowScrollEvent, setAllowScrollEvent] = useState(false);
  const isScroll = useAppSelector(selectIsScroll);
  const [isRender, setIsRender] = useState(isScroll);
  const messageEndRef = useRef<HTMLDivElement>(null);
  const boxMsgRef = useRef<HTMLDivElement>(null);
  const currentUserId = useAppSelector(selectUserId);
  const [getMoreMessage, { data, isSuccess, isLoading }] = useGetMoreMessageMutation();
  const [isFirstCanGetMore, setIsFirstCanGetMore] = useState(true);

  const handleScroll = async () => {
    if (boxMsgRef.current) {
      if (allowScrollEvent) {
        if (boxMsgRef.current.scrollTop < 100 && props.conversations?.canGetMore) {
          try {
            setAllowScrollEvent(false);
            const dataGetMoreMsg: GetMoreMessage = {
              conversationId: props.conversations.id,
              lastMessageId: props.conversations.conversation[0].id,
              lengthMessages: props.conversations.conversation.length.toString(),
            };
            await getMoreMessage(dataGetMoreMsg).unwrap();
          } catch (error) {
            console.log(error);
          }
        }
      }
    }
  };

  useEffect(() => {
    if(props.conversations && isFirstCanGetMore) {
      setAllowScrollEvent(true);
      setIsFirstCanGetMore(false);
    }
  }, [isFirstCanGetMore, props.conversations]);

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setListMessageToConversation(data));
      setAllowScrollEvent(true);
    }
  }, [data, dispatch, isSuccess]);

  useEffect(() => {
    const timer = setTimeout(() => {
      if (messageEndRef.current) {
        messageEndRef.current.scrollIntoView({ block: 'start', behavior: 'smooth' });
      }
    }, 100);
    if (!isScroll) {
      setIsRender(false);
    }
    if (isRender) {
      dispatch(setIsScrollTrue());
    }
    return () => clearTimeout(timer);
  }, [isRender, dispatch, isScroll]);

  return (
    <>
      {isLoading && (
        <Box sx={{ display: 'flex', m: '0 auto' }}>
          <CircularProgress />
        </Box>
      )}
      <Grid
        item
        display="flex"
        alignItems="flex-end"
        sx={{
          height: '743px',
          color: '#E4E6EB',
          paddingTop: '12px',
        }}
      >
        <Box
          sx={{ pl: 1, pb: 2, maxHeight: '100%', width: '100%', overflowY: 'auto' }}
          ref={boxMsgRef}
          onScroll={handleScroll}
        >
          {props.conversations?.conversation.map((conv, index) => {
            if (!isRender) {
              setIsRender(true);
            }
            let isAvatar = false;
            let lastSeenId = '';

            if (props.conversations) {
              if (
                props.conversations.conversation[index]?.receiverId !==
                props.conversations.conversation[index + 1]?.receiverId
              ) {
                isAvatar = true;
              }
              const listIdIsSeen = props.conversations.conversation
                .filter((x) => x.isSeen === true && x.senderId === currentUserId)
                .map((x) => x.id);
              lastSeenId = listIdIsSeen[listIdIsSeen.length - 1];
            }

            return (
              <ConversationMessage
                key={conv.id}
                me={conv.senderId === currentUserId}
                message={conv}
                isAvatar={isAvatar}
                lastSeen={lastSeenId}
              />
            );
          })}
          <Typography component="div" ref={messageEndRef} />
        </Box>
      </Grid>
    </>
  );
};

export default ConversationContent;
