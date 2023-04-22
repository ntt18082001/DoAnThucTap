import { Grid } from "@mui/material";
import { useAppSelector } from "app/hooks";
import { selectIsDarkmode } from "features/darkmode/darkmodeSlice";
import React, { useEffect } from "react";
import { borderColorDarkmode, borderColorDefault } from '../../../constants/index';
import Conversation from "./Conversation/Conversation";
import { useAppDispatch } from '../../../app/hooks';
import { useParams } from "react-router-dom";
import { selectSelectedUser, setSelectedUser, selectCurrentUserId, setListMessage } from '../messageSlice';
import axiosClient from 'api/axiosClient';

type Props = {};

const MessageContent = (props: Props) => {
	console.log("Msg content render");
	const isDarkmode = useAppSelector(selectIsDarkmode);

	const dispatch = useAppDispatch();
	const currentUserId = useAppSelector(selectCurrentUserId);

	const { id } = useParams();
	// const user = useAppSelector(state => state.auth.listUserTalked.find(item => item.id.toString() === id));

	const selectedUser = useAppSelector(selectSelectedUser);
	useEffect(() => {
		if (!selectedUser) {
			// dispatch(setSelectedUser(user));
		}
	}, [selectedUser, dispatch]);

   useEffect(() => {
		const getListMessage = async () => {
			const response = await axiosClient.get(`/message/getlistmessage?senderId=${currentUserId}&receiverId=${selectedUser?.id}`)
				.then(res => {
					return res.data;
				});
				console.log(response);
			dispatch(setListMessage(response));
		}
		getListMessage();
		
   }, [currentUserId, dispatch, selectedUser?.id]);

	const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;

	return (
		<Grid
			item
			xs={7}
			sm={7}
			md={8}
			lg={9}
			sx={{ borderRight: borderColor }}
		>
			<Conversation />
		</Grid>
	);
};

export default React.memo(MessageContent);
