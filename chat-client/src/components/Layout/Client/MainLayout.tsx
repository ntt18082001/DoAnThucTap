import ApiIcon from '@mui/icons-material/Api';
import { Backdrop, Container, LinearProgress } from '@mui/material';
import React, { Suspense, useContext, useEffect, useState } from 'react';
import { Outlet, useParams } from 'react-router-dom';

import { bodyBgColorBlack, bodyBgColorDefault } from '../../../constants';
import { Header } from '../../Common/Client/Header';
import { useAppDispatch, useAppSelector } from '../../../app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import { HubConnection } from '@microsoft/signalr';
import { ChatHubContext } from 'features/hubs/ChatHubContext';
import { useGetListNotifyQuery } from 'features/notify/notify.service';
import { selectIsLoggedIn, selectUserId } from 'features/auth/authSlice';
import { NotifyCancel, NotifyModel } from 'models/notify.model';
import { cancelNotifyFromSender, setListNotify, setNotify } from 'features/notify/notifySlice';
import { toast } from 'react-toastify';
import {
  setAcceptRequest,
  setCancelReceiverRequest,
  setCancelRequest,
  setReceiverRequest,
  setUnfriend,
} from 'features/friends/friendSlice';
import { Unfriend } from 'models/friend.model';
import { setConversations, setDeleteMessage, setFriendConnected, setFriendDisconnected, setInfoConv, setIsScrollFalse, setMessage, setNicknameConv, setSeenLastMessage, toggleLikeMessage } from 'features/message/messageSlice';
import { ConversationModel, Message, SeenMessage, UpdateInfoConvResponse, UpdateNicknameResponse } from 'models/messages.model';
import {
  useGetListConversationQuery,
  useSeenMessageMutation,
} from 'features/message/message.service';

interface MainLayoutProps {}

const MainLayout: React.FC = (props: MainLayoutProps): JSX.Element => {
  const dispatch = useAppDispatch();
  const darkmode = useAppSelector(selectIsDarkmode);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const backgroundBackdrop = darkmode ? bodyBgColorBlack : bodyBgColorDefault;
  const hubConnection = useContext<HubConnection>(ChatHubContext);
  const isLoggedIn = useAppSelector(selectIsLoggedIn);
  const { data, isSuccess, refetch } = useGetListNotifyQuery();
  const { data: dataConv, isSuccess: isSuccessConv, refetch: refetchConv } = useGetListConversationQuery();

  const { id } = useParams();
  const currentUserId = useAppSelector(selectUserId);
  const [seenMessage, seenMessageResult] = useSeenMessageMutation();

  useEffect(() => {
    if (isLoggedIn) {
      refetch();
      refetchConv();
    }
  }, [isLoggedIn, refetch, refetchConv]);

  useEffect(() => {
    if (isSuccessConv && dataConv) {
      dispatch(setConversations(dataConv));
    }
  }, [dispatch, isSuccessConv, dataConv]);

  useEffect(() => {
    if (isSuccess && data) {
      dispatch(setListNotify(data));
    }
  }, [data, dispatch, isSuccess]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('CancelRequestFromReceiver', (data: string) => {
        dispatch(setCancelRequest(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('CancelRequestFromReceiver');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('CancelRequestFromSender', (data: NotifyCancel) => {
        dispatch(setCancelReceiverRequest(data.senderId));
        dispatch(cancelNotifyFromSender(data.notifyId));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('CancelRequestFromSender');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ReceiveFriendRequest', (data: NotifyModel) => {
        dispatch(setNotify(data));
        dispatch(setReceiverRequest(data.senderId));
        toast.info('Bạn có lời mời kết bạn mới!');
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ReceiveFriendRequest');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('AcceptRequest', (data: NotifyModel) => {
        dispatch(setAcceptRequest(data.receiverId));
        dispatch(setNotify(data));
        toast.success(`'${data.fullName}' đã chấp nhận lời mời kết bạn của bạn.`);
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('AcceptRequest');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('Unfriend', (data: Unfriend) => {
        dispatch(setUnfriend(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('Unfriend');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ReceiveMessage', (data: ConversationModel) => {
        dispatch(setMessage(data));
        dispatch(setIsScrollFalse());
        if (id && currentUserId && id == data.lastMessage.senderId) {
          const handleSeenMessageInCurrentParams = async (message: Message) => {
            try {
              const seenMsg: SeenMessage = {
                senderId: message.senderId,
                receiverId: message.receiverId,
                id: message.id,
              };
              await seenMessage(seenMsg).unwrap();
            } catch (error) {
              console.log(error);
            }
          };
          handleSeenMessageInCurrentParams(data.lastMessage);
        }
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ReceiveMessage');
      }
    };
  }, [currentUserId, dispatch, hubConnection, id, seenMessage]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ReceiveSeenMessage', (data: string) => {
        dispatch(setSeenLastMessage(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ReceiveSeenMessage');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ToggleLikeMessage', (data: string) => {
        dispatch(toggleLikeMessage(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ToggleLikeMessage');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('DeleteMessage', (data: string) => {
        dispatch(setDeleteMessage(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('DeleteMessage');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('FriendConnected', (data: string) => {
        dispatch(setFriendConnected(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('FriendConnected');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('FriendDisconnected', (data: string) => {
        dispatch(setFriendDisconnected(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('FriendDisconnected');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ReceiveNotifyMessage', (data: UpdateInfoConvResponse) => {
        dispatch(setInfoConv(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ReceiveNotifyMessage');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (hubConnection) {
      hubConnection.on('ReceiveNotifyMessageNickname', (data: UpdateNicknameResponse) => {
        dispatch(setNicknameConv(data));
      });
    }
    return () => {
      if (hubConnection) {
        hubConnection.off('ReceiveNotifyMessageNickname');
      }
    };
  }, [dispatch, hubConnection]);

  useEffect(() => {
    if (seenMessageResult.isSuccess && seenMessageResult.data) {
      dispatch(setSeenLastMessage(seenMessageResult.data));
    }
  }, [dispatch, seenMessageResult.data, seenMessageResult.isSuccess]);

  useEffect(() => {
    const timer = setTimeout(() => {
      setIsLoading(true);
    }, 1500);

    return () => {
      clearTimeout(timer);
    };
  });

  return (
    <>
      {isLoading ? (
        <div
          style={{
            width: '100%',
          }}
          className={darkmode ? 'darkmode-color' : ''}
        >
          <Header />
          <Container
            maxWidth="xl"
            sx={{
              height: 'calc(100vh - 65px)',
              pl: '0px !important',
              pr: '0px !important',
              maxWidth: '100% !important',
              overflowY: 'scroll',
            }}
            className={darkmode ? 'darkmode-bg-color' : ''}
          >
            <Suspense fallback={<LinearProgress color="secondary" />}>
              <Outlet />
            </Suspense>
          </Container>
        </div>
      ) : (
        <Backdrop
          sx={{
            backgroundColor: backgroundBackdrop,
            zIndex: (theme) => theme.zIndex.drawer + 1,
          }}
          open
        >
          <ApiIcon color="secondary" sx={{ fontSize: 60 }} />
        </Backdrop>
      )}
    </>
  );
};

export default React.memo(MainLayout);
